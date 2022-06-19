using Infrostructure;
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
using System.Windows.Shapes;

namespace PE_Shtabkin
{
    /// <summary>
    /// Класс Client
    /// Содержит методы для взаимодействия с формой
    /// <list type="bullet">
    /// <item>
    /// <term>Car_Context</term>
    /// <description>Контекст машин с БД</description>
    /// </item>
    ///  <item>
    /// <term>GeneralLoad</term>
    /// <description>Загрузка пользователей из контекста с условием</description>
    /// </item>
    /// <item>
    /// <term>Client</term>
    /// <description>Инициализация формы</description>
    /// </item>
    /// <item>
    /// <term>Update</term>
    /// <description>Обновление таблицы с машинами</description>
    /// </item>
    /// <item>
    /// <term>UploadToComboboxes</term>
    /// <description>Выгрузка доступных значений для фильтрации в списки</description>
    /// </item>
    /// <item>
    /// <term>OrderBy</term>
    /// <description>Выполнение сортировки по выбранным параметрам</description>
    /// </item>
    /// <item>
    /// <term>button_Click</term>
    /// <description>Вызов формы авторизации</description>
    /// </item>
    /// <item>
    /// <term>button_Copy_Click</term>
    /// <description>Выход и приложения</description>
    /// </item>
    /// <item>
    /// <term>button_Copy1_Click</term>
    /// <description>Заказ автомобиля</description>
    /// </item>
    /// <item>
    /// <term>comboBox_Copy2_SelectionChanged</term>
    /// <description>Событие смены вида сортировки</description>
    /// </item>
    /// <item>
    /// <term>comboBox_SelectionChanged</term>
    /// <description>Вывод машин в выбранной стране</description>
    /// </item>
    /// <item>
    /// <term>comboBox_Copy_SelectionChanged</term>
    /// <description>Фильтрация машин по городу</description>
    /// </item>
    /// <item>
    /// <term>comboBox_Copy1_SelectionChanged</term>
    /// <description>Фильтрация машин по месту</description>
    /// </item>
    /// <item>
    /// <term>dataGrid_SelectedCellsChanged</term>
    /// <description>Загрузка выбранного тарифа</description>
    /// </item>
    /// <item>
    /// <term>UploatToTextBoxes</term>
    /// <description>Загрузка выбранной машины в поля</description>
    /// </item>
    /// <item>
    /// <term>textBox_TextChanged</term>
    /// <description>Событие изменения арендных дней в поле</description>
    /// </item>
    /// <item>
    /// <term>textBox_Copy_TextChanged</term>
    /// <description>Событие изменения арендных дней в поле</description>
    /// </item>
    /// <item>
    /// <term>button_Copy1_Click_1</term>
    /// <description>Обновление списка</description>
    /// </item>
    public partial class Client : Window
    {
        private Car_Context _cars_context;
        Car[] SelectedCars;
        Car Result;
        MainWindow mainWindow;
        
        /// <summary>
        /// Загрузка машин из контекста с условием
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        void GeneralLoad()
        {
            _cars_context = new Car_Context();
            var res = from iidd in _cars_context.Cars
                      where iidd.Is_Busy == false
                      select iidd;
            SelectedCars = res.ToArray();
        }
        /// <summary>
        /// Инициализация формы
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        public Client()
        {
            InitializeComponent();
            GeneralLoad();
            Update();
            UploadToComboboxes();
            //label_Copy5 - лейбл с ценой
        }
        /// <summary>
        /// Обновление таблицы с машинами
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        void Update()
        {
            dataGrid.ItemsSource = SelectedCars.ToArray();
        }
        /// <summary>
        /// Выгрузка доступных значений для фильтрации в списки
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        private void UploadToComboboxes()
        {
            var rsr = from iiDD in _cars_context.Cars
                      where iiDD.Country != null
                      select iiDD.Country;
            comboBox.ItemsSource = rsr.ToList().Distinct();
            rsr = from iiDD in _cars_context.Cars
                  where iiDD.Town != null
                  select iiDD.Town;
            comboBox_Copy.ItemsSource = rsr.ToList().Distinct();
            rsr = from iiDD in _cars_context.Cars
                  where iiDD.Place != null
                  select iiDD.Place.ToString();
            comboBox_Copy1.ItemsSource = rsr.ToList().Distinct();
        }
        /// <summary>
        /// Выполнение сортировки по выбранным параметрам
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        void OrderBy()
        {
            // страна город цена место
            switch (comboBox_Copy2.SelectedIndex)
            {
                case 0:
                    dataGrid.ItemsSource = SelectedCars.OrderBy(x => x.Country);
                    break;
                case 1:
                    dataGrid.ItemsSource = SelectedCars.OrderBy(x => x.Town);
                    break;
                case 2:
                    dataGrid.ItemsSource = SelectedCars.OrderBy(x => x.Price_PerDay);
                    break;
                case 3:
                    dataGrid.ItemsSource = SelectedCars.OrderBy(x => x.Place);
                    break;
            }
        }
        /// <summary>
        /// Вызов формы авторизации
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //auth
            mainWindow = new MainWindow();
            mainWindow.Show();
            if (Session.Premission_Level == 0)
            {
                Admin _admin = new Admin();
                _admin.Show();
            }
        }
        /// <summary>
        /// Выход из приложения
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            //exit
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Заказ автомобиля
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            // заказать
            try
            {
                Result.Rent_Starts = DateTime.Parse(textBox.Text);
                Result.Rent_Ends = DateTime.Parse(textBox_Copy.Text);
                Result.Is_Busy = true;
                _cars_context.SaveChanges();
                SelectedCars = (from iidd in SelectedCars
                               where iidd.Is_Busy == false
                               select iidd).ToArray();
                Update();
            } catch (Exception ere) { MessageBox.Show(ere.ToString(), "Ошибка"); }
        }
        /// <summary>
        /// Событие смены вида сортировки
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void comboBox_Copy2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // сортировка по
            if (SelectedCars != null)
                OrderBy();
            else
                MessageBox.Show("Не выбраны фильтры","Внимание");
        }
        /// <summary>
        /// Вывод машин в выбранной стране
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // вывод машин в выбранной стране
            SelectedCars = _cars_context.Cars.Where(x => x.Country == comboBox.SelectedItem.ToString()).ToArray();
            Update();
        }
        /// <summary>
        /// Вывод машин в выбранном городе
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void comboBox_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // город
            MessageBox.Show(comboBox_Copy.SelectedItem.ToString());
            SelectedCars = _cars_context.Cars.Where(x => x.Town == comboBox_Copy.SelectedItem.ToString()).ToArray();
            Update();
        }
        /// <summary>
        /// Фильтрация машин по месту
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void comboBox_Copy1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // место
            SelectedCars = _cars_context.Cars.Where(x => x.Place == comboBox_Copy1.SelectedItem.ToString()).ToArray();
            Update();
        }
        /// <summary>
        /// Загрузка выбранного тарифа
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Result = _cars_context.Cars.Where(x => x.ID == (dataGrid.SelectedItem as Car).ID).FirstOrDefault();
        }
        /// <summary>
        /// Загрузка выбранной машины в поля
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="Result">Car</param>
        private void UploatToTextBoxes(Car Result)
        {
            try
            {
                decimal summ = Result.Price_PerDay *
                Convert.ToInt32((DateTime.Parse(textBox_Copy.Text) - DateTime.Parse(textBox.Text)).TotalDays);
                label_Copy5.Content = "Цена " + summ;
            } catch (Exception sdd) { };
        }
        /// <summary>
        /// Событие изменения арендных дней в поле
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UploatToTextBoxes(Result);
        }
        /// <summary>
        /// Событие изменения арендных дней в поле
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void textBox_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            UploatToTextBoxes(Result);
        }
        /// <summary>
        /// Обновление списка
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void button_Copy1_Click_1(object sender, RoutedEventArgs e)
        {
            //обновить
            GeneralLoad();
            SelectedCars = (from iidd in SelectedCars
                            where iidd.Is_Busy == false
                            select iidd).ToArray();
            Update();
        }
    }
}
