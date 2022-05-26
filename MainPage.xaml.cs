using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
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
        List<AppBarButton> fieldButtons = new List<AppBarButton>();
        TheGame game;
        List<SymbolIcon> emptyIcons = new List<SymbolIcon>();
        List<SymbolIcon> plusIcons = new List<SymbolIcon>();
        List<SymbolIcon> minusIcons = new List<SymbolIcon>();
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(400, 400);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            game = new TheGame(3);

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

                //every icon belongs to its button
                emptyIcons.Add(new SymbolIcon(Symbol.Stop));
                plusIcons.Add(new SymbolIcon(Symbol.Accept));
                minusIcons.Add(new SymbolIcon(Symbol.Cancel));
            }

            UpdateField();
        }

        public void UpdateField()
        {
            for (int i = 0; i < fieldButtons.Count; i++)
            {
                switch (game.GetCell(i))
                {
                    case '+':
                        fieldButtons[i].Icon = plusIcons[i];
                        break;
                    case '-':
                        fieldButtons[i].Icon = minusIcons[i];
                        break;
                    case '0':
                        fieldButtons[i].Icon = emptyIcons[i];
                        break;
                    default:
                        throw new Exception("Bruh");
                }
            }
        }

        private void MakeMove(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < fieldButtons.Count; i++)
            {
                if (sender == fieldButtons[i])
                {
                    if(game.GetCell(i) == '0')
                    {
                        game.MakeMove(i);
                        UpdateField();
                        var gameState = game.GetGameState();
                        if (gameState != GameState.Continue)
                        {
                            ShowEndMessage(gameState);
                            game.Restart();
                            UpdateField();
                            return;
                        }
                        else
                        {
                            game.EnemyMove();
                            gameState = game.GetGameState();
                            if (gameState != GameState.Continue)
                            {
                                ShowEndMessage(gameState);
                                game.Restart();
                            }
                        }
                        UpdateField();
                    }
                    return;
                }
            }
        }

        private async void ShowEndMessage(GameState gameState)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Game is over",
                CloseButtonText = "Ok"
            };

            switch (gameState)
            {
                case GameState.PlusWons:
                    dialog.Content = "You won!!!";
                    break;
                case GameState.MinusWons:
                    dialog.Content = "You lost...";
                    break;
                case GameState.Draw:
                    dialog.Content = "It's a draw";
                    break;
            }
            var result = await dialog.ShowAsync();
        }
    }
}
