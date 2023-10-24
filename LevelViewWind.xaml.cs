using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using StudyRoomPlugin.Model;
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
	/// Логика взаимодействия для LevelViewWind.xaml
	/// </summary>
	public partial class LevelViewWind : Window
	{
		List<Level> allLevels;
		Room[] allRooms;
		Document _doc;
		ListBox _AllRoomsView;
		public LevelViewWind(List<Level> levels, Room[] rooms, Document doc, UIElement AllRoomsView)
		{
			InitializeComponent();
			allLevels = levels;
			allRooms = rooms;
			_doc = doc;
			_AllRoomsView = AllRoomsView;
			//создаем checkbox с уровнями
			foreach (Level level in allLevels)
			{

				CheckBox checklevel = new CheckBox();

				checklevel.Content = level.Name;
				AllLevelsView.Children.Add(checklevel);
			}
			
		}
		private void Start_Click(object sender, RoutedEventArgs e)
		{
			double startValue = Convert.ToDouble(StartNumberValueView.Text.Replace(".", ","));

			Dictionary<string, List<Room>> allRoomsByLevels = new Dictionary<string, List<Room>>();
			foreach(Room room in allRooms)
			{
				string levelRoomName = room.Level.Name;
				if (allRoomsByLevels.ContainsKey(levelRoomName))
				{
					allRoomsByLevels[levelRoomName].Add(room);
				}
				else
				{
					List<Room> rooms = new List<Room>() { room };
					allRoomsByLevels[levelRoomName] = rooms;
				}
			}

			UIElementCollection comboBoxes = AllLevelsView.Children;
			
			List<String> allCheckedLevels = new List<string>();
			
			foreach (UIElement element in comboBoxes)
			{
				CheckBox checkBox = element as CheckBox;
                     if (checkBox.IsChecked != null)
					allCheckedLevels.Add(checkBox.Content.ToString()); 
			}
			using(Transaction t = new Transaction(_doc))
			{
				t.Start("Set.Number");
				foreach(string level in allCheckedLevels)
				{
					List<Room> chechedRooms = allRoomsByLevels[level];
			for(int i = 0; i < allRooms.Count(); i++)
			{
				Room room = allRooms[i];
				string roomLevelName = room.Level.Name;
				if (allCheckedLevels.Contains(roomLevelName))
					{

						double newNumber = 0;
						double addNum = Math.Round(startValue - Math.Truncate(startValue), 3);
							
							if (addNum == 0)
								newNumber = startValue +i;
							else
							{
								string checkAddNum = addNum.ToString();
								int a = Convert.ToInt32(addNum.ToString().Split(',')[1]);
								double newStep = Convert.ToDouble(addNum / a);
								newNumber = startValue + newStep*i;
							}
						room.get_Parameter(BuiltInParameter.ROOM_NUMBER).Set(newNumber.ToString().Replace(".", ","));
					}
					

			}
				}
				t.Commit();
			}

			_AllRoomsView.Items.Clear();
			Tuple<Room[], List<Level>> resultNew = WorkClass.GetAllRooms(_doc);
			_AllRoomsView.ItemsSource = resultNew.Item1;
		}
	}

	
}
