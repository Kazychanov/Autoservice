using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kazychanov_Autoservice
{
  /// <summary>
  /// Логика взаимодействия для SignUpPage.xaml
  /// </summary>
  public partial class SignUpPage : Page
  {
    private Service _currentService = new Service();
    public SignUpPage(Service SelectedService)
    {
      InitializeComponent();
      if (SelectedService != null)
        this._currentService = SelectedService;

      DataContext = _currentService;
      var _currentClient = KazychanovAutoServiceEntities.GetContext().Client.ToList();
      ComboClient.ItemsSource = _currentClient;
    }


    //При инициализации установки DataContext страницы - этот созданный объект чтобы на форму загрузить выбранные наименование услуги и длительность

    private ClientService _currentClientService = new ClientService();
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
      StringBuilder errors = new StringBuilder();

      if (ComboClient.SelectedItem == null)
        errors.AppendLine("Укажите ФИО клиента");

      if (StartDate.Text == "")
        errors.AppendLine("Укажите дату услуги");

      if (Convert.ToDateTime(StartDate.Text) < DateTime.Now)
        errors.AppendLine("Нельзя записаться в прошлое");

      if (TBStart.Text == "")
        errors.AppendLine("Укажите время начала услуги");

      if (!Regex.IsMatch(TBStart.ToString(), @"^[0-9]||[0-1][0-9]||2[0-3]:[0-5][0-9]$"))
        errors.AppendLine("Указано неверное время начала услуги");

      if (!Regex.IsMatch(TBEnd.ToString(), @"^[0-9]||[0-1][0-9]|2[0-3]:[0-5][0-9]$"))
        errors.AppendLine("Указано неверное время конца услуги");

      if (errors.Length > 0)
      {
        MessageBox.Show(errors.ToString());
        return;
      }


      _currentClientService.ClientID = ComboClient.SelectedIndex + 1;
      _currentClientService.ServiceID = _currentService.id;
      _currentClientService.StartTime = Convert.ToDateTime(StartDate.Text + " " + TBStart.Text);
      if (_currentClientService.ID == 0)
        KazychanovAutoServiceEntities.GetContext().ClientService.Add(_currentClientService);

      try
      {
        KazychanovAutoServiceEntities.GetContext().SaveChanges();
        MessageBox.Show("Информация сохранена");
        Manager.MainFrame.GoBack();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString());
      }
    }

    private void TBStart_TextChanged(object sender, TextChangedEventArgs e)
    {
      string s = TBStart.Text;
      string newText = string.Empty;

      foreach (char c in s)
      {
        if (char.IsDigit(c) || c == ':')
        {
          newText += c;
        }
      }

      TBStart.Text = newText;
      TBStart.SelectionStart = newText.Length;

      if (newText.Length > 5 || newText.Length < 3 || !newText.Contains(':') || newText[newText.Length - 1] == ':')
      {
        TBEnd.Text = "";
      }
      else
      {
        string[] start = newText.Split(':');
        int startHour = Convert.ToInt32(start[0]) * 60;
        int startMin = Convert.ToInt32(start[1]);

        int sum = startHour + startMin + _currentService.Duration;

        int endHour = sum / 60;
        int endMin = sum % 60;
        s = $"{endHour}:{endMin:00}";
        TBEnd.Text = s;
      }
    }


    private void TBEnd_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
  }
}
