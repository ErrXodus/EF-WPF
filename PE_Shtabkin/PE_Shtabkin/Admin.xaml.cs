using Infrostructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PE_Shtabkin
{
    /// <summary>
    /// Класс Admin
    /// Содержит методы для взаимодействия с формой
    /// <list type="bullet">
    /// <item>
    /// <term>Admin</term>
    /// <description>Инициализация формы и загрзка контекста</description>
    /// </item>
    /// <item>
    /// <term>Updape</term>
    /// <description>Получение и вывод машин</description>
    /// </item>
    /// <item>
    /// <term>button_Click</term>
    /// <description>Добавление машин через шаблон</description>
    /// </item>
    /// <item>
    /// <term>UploadToComboboxes</term>
    /// <description>Загрзка возможных вариантов характеристик тарифа</description>
    /// </item>
    ///  <item>
    /// <term>UploatToTextBoxes</term>
    /// <description>Загрузка выбранной машины в поля</description>
    /// </item>
    /// <item>
    /// <term>dataGrid_SelectedCellsChanged</term>
    /// <description>Загрузка выбранной машины</description>
    /// </item>
    ///  <item>
    /// <term>button_Copy1_Click_1</term>
    /// <description>Добавление новой машины</description>
    /// </item>
    public partial class Admin : Window
    {
        private Car_Context _cars_context;
        Car[] SelectedCars;
        Car Result;
        /// <summary>
        /// Инициализация формы и загрзка контекста
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        public Admin()
        {
            InitializeComponent();
            _cars_context = new Car_Context();
            Updape();
            UploadToComboboxes();
        }
        /// <summary>
        /// Получение и вывод машин
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        void Updape()
        {
            SelectedCars = _cars_context.Cars.ToArray();
            dataGrid.ItemsSource = SelectedCars.OrderBy(x => x.ID);
        }
        /// <summary>
        /// Добавление машин через шаблон
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            // добавить cуществующий
            // страна город цена место
            //decimal d = Convert.ToDecimal(comboBox_Copy2.Text);
            Car New_Car = new Car();
            New_Car.Country = comboBox.SelectedValue.ToString() is null ? "null" : comboBox.SelectedValue.ToString();
            New_Car.Town = comboBox_Copy.SelectedValue.ToString() is null ? "null" : comboBox_Copy.SelectedValue.ToString();
            New_Car.Place = comboBox_Copy1.SelectedValue.ToString() is null ? "null" : comboBox_Copy1.SelectedValue.ToString();
            New_Car.Price_PerDay = Convert.ToDecimal(comboBox_Copy2.Text.Split('.')[0]);
            _cars_context.Add(New_Car);
            _cars_context.SaveChanges();
            Updape();
            UploadToComboboxes();
        }
        /// <summary>
        /// Загрзка возможных вариантов характеристик тарифа
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        private void UploadToComboboxes()
        {// страна город место цена за день марка
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
            rsr = from iiDD in _cars_context.Cars
                  where iiDD.Price_PerDay != 0//null не может быть а еще я гей
                  select iiDD.Price_PerDay.ToString();
            comboBox_Copy2.ItemsSource = rsr.ToList().Distinct();
            rsr = from iiDD in _cars_context.Cars
                      where iiDD.Model != null
                      select iiDD.Model;
            comboBox_Copy3.ItemsSource = rsr.ToList().Distinct();
            //rsr = from iiDD in _cars_context.Cars
            //      where iiDD.Town != null
            //      select iiDD.Town;
            //comboBox_Copy.ItemsSource = rsr.ToList();
        }
        /// <summary>
        /// Изменение сушествующего тарифа
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            // изменить
            Result.Country = textBox.Text;
            Result.Town = textBox_Copy.Text;
            Result.Place = textBox_Copy1.Text;
            Result.Price_PerDay = Convert.ToDecimal(textBox_Copy2.Text);
            Result.Model = textBox_Copy3.Text;
            if (!string.IsNullOrEmpty(textBox_Copy4.Text))
                Result.Rent_Starts = Convert.ToDateTime(textBox_Copy4.Text);
            if (!string.IsNullOrEmpty(textBox_Copy5.Text))
                Result.Rent_Ends = Convert.ToDateTime(textBox_Copy5.Text);
            _cars_context.SaveChanges();
            Updape();
            UploadToComboboxes();
        }
        /// <summary>
        /// Загрузка выбранной машины в поля
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="Result">Car</param>
        private void UploatToTextBoxes(Car Result)
        {
            textBox.Text = Result.Country;
            textBox_Copy.Text = Result.Town;
            textBox_Copy1.Text = Result.Place;
            textBox_Copy2.Text = Result.Price_PerDay.ToString();
            textBox_Copy3.Text = Result.Model;
            if (!string.IsNullOrEmpty(Result.Rent_Starts.ToString()))
                textBox_Copy4.Text = Result.Rent_Starts.ToString();
            if (!string.IsNullOrEmpty(Result.Rent_Ends.ToString()))
                textBox_Copy5.Text = Result.Rent_Ends.ToString();
        }
        /// <summary>
        /// Загрузка выбранной машины
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Result = _cars_context.Cars.Where(x => x.ID == (dataGrid.SelectedItem as Car).ID).FirstOrDefault();
            UploatToTextBoxes(Result);
        }
        /// <summary>
        /// Добавление новой машины
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void button_Copy1_Click_1(object sender, RoutedEventArgs e)
        {
            // добавить новую машину
            //страна город место цена марка
            string s="",ss="";
            if (!string.IsNullOrEmpty(textBox_Copy4.Text))
                s = textBox_Copy4.Text;
            if (!string.IsNullOrEmpty(textBox_Copy5.Text))
                ss = textBox_Copy5.Text;
            try
            {
                _cars_context.Add(new Car()
                {
                    Country = textBox.Text,
                    Town = textBox_Copy.Text,
                    Place = textBox_Copy1.Text,
                    Price_PerDay = Convert.ToDecimal(textBox_Copy2.Text),
                    Model = textBox_Copy3.Text,
                    Rent_Starts = Convert.ToDateTime(s),
                    Rent_Ends = Convert.ToDateTime(ss)
                });
                _cars_context.SaveChanges();
                Updape();
            }
            catch (Exception ara)
            { MessageBox.Show(ara.Message + "\n" + ara.ToString(), "Обнаружена ошибка"); }
        }
    }
}
