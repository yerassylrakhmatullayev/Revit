using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace StudyRoomPlugin
{
	/// <summary>
	/// Логика взаимодействия для UserWindRoom.xaml
	/// </summary>
	public partial class UserWindRoom : Window
	{
		Room[] allRooms;
		List<Level> allLevels;
		Document _doc;
		public UserWindRoom(Room[]  rooms, List<Level> Levels, Document doc)
		{
			InitializeComponent();
			allRooms = rooms;
			allLevels = allLevels;
			_doc = doc;
			AllRoomsView.ItemsSource = allRooms;
			AllRoomsView.DisplayMemberPath = "Name";
		}

		private void SortRoomsInProject(Object sender, EventArgs e)
		{
			LevelViewWind levelWind = new LevelViewWind(allLevels, allRooms, _doc, AllRoomsView);
			levelWind.ShowDialog();
			//MessageBox.Show("Все работает!");
		}
		
	}
}
