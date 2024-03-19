using System.Collections.Generic;
using BlackKakadu.DecorateTheRoom.Ui.Views;
using JetBrains.Annotations;

namespace BlackKakadu.DecorateTheRoom.Ui.Models
{
    [UsedImplicitly]
    public class MenuButtonsModel
    {
        public Dictionary<string, MenuButton> MenuButtons { get; } = new Dictionary<string, MenuButton>();

        public void Add(string itemId, MenuButton menuButton)
        {
            MenuButtons.Add(itemId, menuButton);
        }
    }
}