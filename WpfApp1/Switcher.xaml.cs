using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Switcher.xaml
    /// </summary>
    public partial class Switcher : UserControl
    {
        public static Switcher SwitchForm;

        public string languagePath = "LanguageSelector/ru-Ru.xaml";
        public string stylePath = "StyleSelector/DarkTheme.xaml";

        public Switcher()
        {
            InitializeComponent();
            SwitchForm = this;
            ResourceDictionary language = new ResourceDictionary();
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);

            ResourceDictionary style = new ResourceDictionary();
            style.Source = new Uri(stylePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(style);


        }
        private void Lang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lang.SelectedItem == Russian) SwitchLanguageRussian();
            else
            {
                SwitchLanguageEng();
            }
        }
        public void SwitchLanguageRussian() // Меняем язык на русский
        {
            languagePath = "LanguageSelector/ru-RU.xaml";
            ResourceDictionary language = new ResourceDictionary();
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);
            if (MainWindow.MainForm != null)
                MainWindow.MainForm.SwitchLanguageRussian();
        }

        public void SwitchLanguageEng() // Меняем язык на англ
        {
            languagePath = "LanguageSelector/en-US.xaml";
            ResourceDictionary language = new ResourceDictionary();
            language.Source = new Uri(languagePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(language);
            if(MainWindow.MainForm !=null)
            MainWindow.MainForm.SwitchLanguageEng();
        }

        private void Style_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Theme.SelectedItem == White) SetWhiteStyle();
            else
            {
                SetBlackStyle();
            }
        }

        public void SetBlackStyle()
        {
            stylePath = "StyleSelector/DarkTheme.xaml";
            ResourceDictionary style = new ResourceDictionary();
            style.Source = new Uri(stylePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(style);
            if (MainWindow.MainForm != null)
                MainWindow.MainForm.SetBlackStyle();
        }
        public void SetWhiteStyle()
        {
            stylePath = "StyleSelector/WhiteTheme.xaml";
            ResourceDictionary style = new ResourceDictionary();
            style.Source = new Uri(stylePath, UriKind.Relative);
            Resources.MergedDictionaries.Add(style);
            if (MainWindow.MainForm != null)
                MainWindow.MainForm.SetWhiteStyle();
        }


    }
}
