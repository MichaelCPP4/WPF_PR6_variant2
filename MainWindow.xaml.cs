using Lib_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace WPF_PR5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //int kol = 0;

        //List<Object> cars = new List<Object>();

        Car car;
        object[] list;

        public MainWindow()
        {
            InitializeComponent();

            comboBox1.MaxDropDownHeight = 100;
            comboBox1.ItemsSource = listBoxCars.Items;
            comboBox2.MaxDropDownHeight = 100;
            //comboBox2.ItemsSource = listBoxCars.Items;
            comboBox2.ItemsSource = list;
        }

        void Print(Car car)
        {
            textBoxOutLog.Text = $"Марка: {car.Marka ?? "Без названия"}" +
                    $"\nКол-во целиндров: {car.NumOfCylinders}"
                    + $"\nМощность двигателя в Л.С.: {car.HorsePower}"
                    + $"\nМощность двигателя в  кВт: {car.KW}";
        }

        private void ZapisButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCars.SelectedItem != null)
            {
                car = listBoxCars.SelectedItem as Car;

                double kW;
                int numCylinders;

                if (int.TryParse(textBoxNumCylinders.Text, out numCylinders) && double.TryParse(textBoxkW.Text, out kW))
                {
                    car.SetParams(textBoxMarka.Text, numCylinders, kW);
                }
                else if (int.TryParse(textBoxNumCylinders.Text, out numCylinders))
                {
                    car.KW = new double();
                    car.SetParams(textBoxMarka.Text, numCylinders);
                }
                else if (double.TryParse(textBoxkW.Text, out kW))
                {
                    car.NumOfCylinders = new int();
                    car.SetParams(textBoxMarka.Text, kW);
                }
                else
                {
                    car.SetParams(textBoxMarka.Text, new int(), new double());
                }
                listBoxCars.Items.Insert(listBoxCars.SelectedIndex, car);
                listBoxCars.Items.RemoveAt(listBoxCars.SelectedIndex);
                comboBox1.ItemsSource = listBoxCars.Items;

                list = listBoxCars.Items.OfType<object>().ToArray();
                comboBox2.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("Выберите машину","Error!");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxMarka.Text != string.Empty)
            {
                double kW;
                int numCylinders;
                listBoxCars.Items.Add(new Car());

                car = listBoxCars.Items[listBoxCars.Items.Count - 1] as Car;


                if (int.TryParse(textBoxNumCylinders.Text, out numCylinders) && double.TryParse(textBoxkW.Text, out kW))
                {
                    car.SetParams(textBoxMarka.Text, numCylinders, kW);
                }
                else if (int.TryParse(textBoxNumCylinders.Text, out numCylinders))
                {
                    car.KW = new double();
                    car.SetParams(textBoxMarka.Text, numCylinders);
                }
                else if (double.TryParse(textBoxkW.Text, out kW))
                {
                    car.NumOfCylinders = new int();
                    car.SetParams(textBoxMarka.Text, kW);
                }
                else
                {
                    car.SetParams(textBoxMarka.Text, new int(), new double());
                }

            }
            else
            {
                MessageBox.Show("Введите название для машины", "Error!");
            }

            list = listBoxCars.Items.OfType<object>().ToArray();
            comboBox2.ItemsSource = list;
            comboBox1.SelectedIndex = 0;
        }

        private void VivodButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCars.SelectedItem != null)
            {
                car = listBoxCars.SelectedItem as Car;

                Print(car);
            }
            else
            {
                MessageBox.Show("Выберите машину", "Error!");
            }
        }

        private void SravnitButton_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                Car car1 = comboBox1.SelectedItem as Car;
                Car car2 = comboBox2.SelectedItem as Car;

                if (car1 > car2)
                {
                    textBoxOutLog2.Text = $"Автомобиль {car1.Marka} круче {car2.Marka}";
                }
                else if(car1 < car2)
                {
                    textBoxOutLog2.Text = $"Автомобиль {car2.Marka} круче {car1.Marka}";
                }
                else if (car1 == car2)
                {
                    textBoxOutLog2.Text = $"Автомобиль {car2.Marka} равен {car1.Marka} по характеристикам";
                }

/*
                string info;

                if (car1 > car2.NumOfCylinders)
                {
                    operator1.Content = ">";
                    info = $"Автомобиль {car1.Marka} лучше {car2.Marka} в кол-ве цилиндров на {car1.NumOfCylinders - car2.NumOfCylinders}.";
                }
                else if (car1 == car2.NumOfCylinders)
                {
                    operator1.Content = "=";
                    info = $"Автомобиль {car1.Marka} равен {car2.Marka} в кол-ве цилиндров.";
                }
                else
                {
                    operator1.Content = "<";
                    info = $"Автомобиль {car1.Marka} устуает {car2.Marka} в кол-ве цилиндров {car2.NumOfCylinders - car1.NumOfCylinders}.";
                }


                if (car1 > car2.KW)
                {
                    operator2.Content = ">";
                    info += $"\nАвтомобиль {car1.Marka} лучше {car2.Marka} в мощности на {car1.KW - car2.KW}.";
                }
                else if (car1 == car2.KW)
                {
                    operator2.Content = "=";
                    info += $"\nАвтомобиль {car1.Marka} равен {car2.Marka} в мощности.";
                }
                else
                {
                    operator2.Content = "<";
                    info += $"\nАвтомобиль {car1.Marka} уступает {car2.Marka} в мощности на {car2.KW - car1.KW}.";
                }
                textBoxOutLog2.Text = info;*/
            }
            else
            {
                MessageBox.Show("Отсутствуют автомобили", "Error!");
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            listBoxCars.Items.Remove(listBoxCars.SelectedItem);

            list = listBoxCars.Items.OfType<object>().ToArray();
            comboBox2.ItemsSource = list;

        }

        private void comboBox1_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                car = comboBox1.SelectedItem as Car;
                textBoxPower1.Text = car.HorsePower.ToString();
                textBoxCylinders1.Text = car.NumOfCylinders.ToString();
            }
            else
            {
                textBoxPower1.Text = String.Empty;
                textBoxCylinders1.Text = String.Empty;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                car = comboBox2.SelectedItem as Car;
                textBoxPower2.Text = car.HorsePower.ToString();
                textBoxCylinders2.Text = car.NumOfCylinders.ToString();
            }
            else
            {
                textBoxPower2.Text = String.Empty;
                textBoxCylinders2.Text = String.Empty;
            }
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Задание:\nИспользовать базовый класс Саr (машина), характеризуемый торговой маркой \r\n(строка), числом цилиндров, мощностью. Разработать операции для определения \r\nкрутости машин. Машина считается круче, если у одной машины количество \r\nцилиндров и мощность больше чем у другой машины или при равенстве одного из \r\nпараметров второй параметр больше. Разработать операцию увеличение мощности \r\nна 1.\r\n", "О программе");
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCars.SelectedItem !=null)
            {
                car = listBoxCars.SelectedItem as Car;
                car++;
                listBoxCars.SelectedItem = car;

                Print(car);

                list = listBoxCars.Items.OfType<object>().ToArray();
                //ItemsControl.ItemsSource
                //comboBox2.DataContext = null;
                //comboBox2.Items.Clear();
                //comboBox1.ItemsSource = list;
                comboBox2.ItemsSource = list;
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Выберите машину, которой будете увеличивать мощность", "Error!");
            }
        }
    }
}