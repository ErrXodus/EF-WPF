using Infrostructure;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PE_Shtabkin
{
    /// <summary>
    /// Класс MainWindow
    /// Содержит методы для взаимодействия с формой
    /// <list type="bullet">
    /// <item>
    /// <term>Person_Context</term>
    /// <description>Контекст пользователей с БД</description>
    /// </item>
    /// <item>
    /// <term>MainWindow</term>
    /// <description>Инициализация формы и загрузка пользователей с контекста</description>
    /// </item>
    /// <item>
    /// <term>button_Click</term>
    /// <description>Регистрация новых пользователей</description>
    /// </item>
    /// <item>
    /// <term>button_Copy_Click</term>
    /// <description>Вход в приложение</description>
    /// </item>
    /// <item>
    /// <term>button_Copy1_Click</term>
    /// <description>Безусловный вход в приложение</description>
    /// </item>
    public partial class MainWindow : Window
    {
        private Person_Context _persons_context;
        /// <summary>
        /// Инициализация формы и загрузка пользователей с контекста
        /// </summary>
        /// /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        public MainWindow()
        {
            InitializeComponent();
            _persons_context = new Person_Context();
        }
        /// <item>
        /// <term>button_Click</term>
        /// <description>Регистрация новых пользователей</description>
        /// </item>
        /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //регистрация
            if (!string.IsNullOrEmpty(textBox.Text) && !string.IsNullOrEmpty(passwordBox.Password))
            {
                _persons_context.Add(new Person() { Login = textBox.Text, Password = passwordBox.Password, Premission_Level = 1 });
                _persons_context.SaveChanges();
            }
            else
                MessageBox.Show("Поля не заполнены", "Внимание");
        }
        /// <item>
        /// <term>button_Copy_Click</term>
        /// <description>Вход в приложение</description>
        /// </item>
        /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            //вход
            Person result = _persons_context.Persons.Where(x => x.Login == textBox_Copy.Text).Where(x => x.Password == passwordBox_Copy.Password).FirstOrDefault();
            Session.Premission_Level = result is null ? 2 : result.Premission_Level; // загрузка premission level
            if (result == null)
                MessageBox.Show("Ошибка входа", "Внимание");
            else
            {
                if (Session.Premission_Level == 1)
                {
                    Client client = new Client();
                    client.Show();
                }
                else
                {
                    Admin __admin = new Admin();
                    __admin.Show();
                }
                this.Close();
            }
        }
        /// <item>
        /// <term>button_Copy1_Click</term>
        /// <description>Безусловный вход в приложение</description>
        /// </item>
        /// <exception cref="System.Exception">
        /// Возникает при отсутствии подключения к базе данных</exception>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Session.Premission_Level = 2;
            Client client = new Client();
            client.Show();
            this.Close();
        }
    }
}
