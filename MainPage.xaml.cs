using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace TicTacToe
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        char[] field = new char[9];
        List<AppBarButton> fieldButtons = new List<AppBarButton>();
        public MainPage()
        {
            this.InitializeComponent();
            ClearField();

            fieldButtons.Add(Cell0);
            fieldButtons.Add(Cell1);
            fieldButtons.Add(Cell2);
            fieldButtons.Add(Cell3);
            fieldButtons.Add(Cell4);
            fieldButtons.Add(Cell5);
            fieldButtons.Add(Cell6);
            fieldButtons.Add(Cell7);
            fieldButtons.Add(Cell8);

            foreach (AppBarButton button in fieldButtons)
            {
                button.HorizontalAlignment = HorizontalAlignment.Center;
                button.VerticalAlignment = VerticalAlignment.Center;
            }

            UpdateField();

        }

        public void ClearField()
        {
            for (int i = 0; i < field.Length; i++)
            {
                field[i] = '0';
            }
        }

        public void UpdateField()
        {
            for (int i = 0; i < fieldButtons.Count; i++)
            {
                switch (field[i])
                {
                    case '+':
                        fieldButtons[i].Icon = new SymbolIcon(Symbol.Accept);
                        break;
                    case '-':
                        fieldButtons[i].Icon = new SymbolIcon(Symbol.Cancel);
                        break;
                    case '0':
                        fieldButtons[i].Icon = new SymbolIcon(Symbol.Stop);
                        break;
                    default:
                        throw new Exception("Bruh");
                }
            }
        }
    }
}
