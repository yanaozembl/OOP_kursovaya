using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.CreateFlat;

namespace WpfApp1.Command
{
    class ApplicationsViewModel
    {
        private RelayCommand createFlat;
        public RelayCommand CreateFlat
        {
            get
            {
                return createFlat ?? (createFlat = new RelayCommand(obj =>
                {
                    CreateFlat.CreateFlat create = new CreateFlat.CreateFlat();
                    create.Show();
                }));
            }
        }

        private RelayCommand showFlats;
        public RelayCommand ShowFlats
        {
            get
            {
                return showFlats ?? (showFlats = new RelayCommand(obj =>
                {
                    ShowFlats create = new ShowFlats();
                    create.Show();
                }));
            }
        }

        private RelayCommand redoButton;

        public RelayCommand RedoButton
        {
            get
            {
                return redoButton ?? (redoButton = new RelayCommand(obj =>
                {
                    MainWindow.MainForm.RedoB();
                }));
            }
        }

        private RelayCommand undoButton;
        public RelayCommand UndoButton
        {
            get
            {
                return undoButton ?? (undoButton = new RelayCommand(obj =>
                {
                    MainWindow.MainForm.UndoB();
                }));
            }
        }
    }
}
