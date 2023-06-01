// LINQ
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Linq_alapok
{
    internal class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set;}
        public string Job { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
           // IENUMERABLE<T> - iterátor tervezési minta
           // LINQ kifejezések erre épülnek
           // T - álltalános típus

            IEnumerable<int> list = new List<int>() { 1,2,3,4};
            IEnumerable<int> array = new[] { 1, 2, 3, 4 };
            IEnumerable<int> set = new SortedSet<int>() {4,2,1 };

            List<People> persons = new List<People>();
            persons.Add(new People() { Id=1, Name="Béla", Age=25, City="Budapest", Job="Developer"});
            persons.Add(new People() { Id=2, Name="Gergő", Age=26, City="Miskolc", Job="IT"});
            persons.Add(new People() { Id=3, Name="Szabolcs", Age=28, City="Sopron", Job="Manager"});
            persons.Add(new People() { Id=4, Name="Dávid", Age=26, City="Budapest", Job="Trainer"});

            // - lekérdező kifejezés (Query expression)
            // - metódus bővítés (Extension method)

            Console.WriteLine("Összes rekord:");
            //IEnumerable<People> eredmeny1 = from p in persons select p;
            IEnumerable<People> eredmeny1 = persons.Select(p => p);

            foreach (var item in eredmeny1)
            {
                Console.WriteLine(item.Name+", "+item.Age);
            }

            Console.WriteLine();
            Console.WriteLine("Adott mező");
            //var eredmeny2 = from p in persons select p.Job;
            var eredmeny2 = persons.Select(p => p.Job);
            foreach (var item in eredmeny2)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine("Új objektum");
            //var eredmeny3 = from p in persons select new { p.Name, p.Age };
            var eredmeny3 = persons.Select(p => new { p.Name, p.Age });
            foreach (var item in eredmeny3)
            {
                Console.WriteLine(item.Name + ", " + item.Age);
            }

            Console.WriteLine();
            Console.WriteLine("Szűrés");
            //var eredmeny4 = from p in persons where p.City == "Budapest" select p;
            //var eredmeny4 = persons.Where(p => p.City == "Budapest");
            //foreach (var item in eredmeny4)
            //{
            //    Console.WriteLine(item.Name+" "+item.City);
            //}
            persons.Where(p => p.City == "Budapest").ToList().ForEach(p => Console.WriteLine(p.Name + " " + p.City));

            Console.WriteLine();
            Console.WriteLine("Rendezés");
            //(from p in persons orderby p.Name select p).ToList().ForEach(persons=> Console.WriteLine(persons.Name));
            //persons.OrderBy(p => p.Name).Select(p => p.Name).ToList().ForEach(p => Console.WriteLine(p));

            persons.OrderBy(p => p.Age).ThenBy(p => p.Name).Select(p => p).ToList().ForEach(p => Console.WriteLine(p.Name));

            Console.WriteLine();
            Console.WriteLine("Ismétlődés");
            (from p in persons select p.City).Distinct().ToList().ForEach(p => Console.WriteLine(p));
            persons.Select(p=> p.City).Distinct().ToList().ForEach(p => Console.WriteLine(p));

            Console.WriteLine();
            Console.WriteLine("statistika");
            int[] tomb = new[] { 1, 2, 3, 4, 5 };

            Console.WriteLine(tomb.Count());
            Console.WriteLine(tomb.Min());
            Console.WriteLine(tomb.Max());
            Console.WriteLine(tomb.Average());
            Console.WriteLine(tomb.Sum());

            Console.WriteLine();
            Console.WriteLine((from p in persons select p.Age).Sum());

            Console.WriteLine();

            List<double?> numbers = new List<double?>() { 2.0, 2.1, 2.2, 2.3};
            // First() / Last()
            Console.WriteLine(numbers.First());
            Console.WriteLine(numbers.Last());

            // First(feltétel) / Last(feltétel) - ha nincs visszaadott érték akkor hibaüzenet!!
            Console.WriteLine(numbers.First(x => x > 2.2));
            Console.WriteLine(numbers.Last(x => x < 2.2));

            Console.WriteLine();
            // FirstOrDefault(feltétel) / LastOrDefault(feltétel)
            if (numbers.FirstOrDefault(x => x > 4) == null)
            {
                Console.WriteLine("Nincs találat");
            }

            Console.WriteLine();
            Console.WriteLine("Lépkedés");
            List<string> animals = new List<string>() { "émalac", "áló", "kecske", "csirke", "házi nyúl" };

            // Take() - hány elemet fegyen figyelembe
            animals.Take(4).ToList().ForEach(a => Console.WriteLine(a));
            Console.WriteLine();
            // Skip() - kihagyja az első N elemet
            animals.Skip(4).ToList().ForEach(a => Console.WriteLine(a));

            Console.WriteLine();
            // TakeWhile() / SkipWhile()
            List<int> numbers2 = new List<int>() { 1, 2, 4, 8, 4, 2, 1 };
            numbers2.TakeWhile(x => x < 5).ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine();
            numbers2.SkipWhile(x => x != 4).ToList().ForEach(x => Console.WriteLine(x));

            Console.WriteLine();
            Console.WriteLine("sorrend fordítás");
            //animals.Reverse();
            //animals.ForEach(a => Console.WriteLine(a));

            List<string> animalsRev = Enumerable.Reverse(animals).ToList();
            animalsRev.ForEach(a => Console.WriteLine(a));

            Console.WriteLine();
            Console.WriteLine();

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("hu-HU");
            animals.OrderBy(x => x, StringComparer.CurrentCulture).ToList().ForEach(a => Console.WriteLine(a));

        }
    }
}

// ENTITY FRAMEWORK EF

// NuGet\Install-Package Microsoft.EntityFrameworkCore -Version 7.0.2
// NuGet\Install-Package Microsoft.EntityFrameworkCore.Tools -Version 7.0.2
// NuGet\Install-Package Pomelo.EntityFrameworkCore.MySql -Version 7.0.0
using Microsoft.EntityFrameworkCore;
namespace wpf_24_ef_alapok
{
    public class People
    {
        [Key]
        public int id { get; set; }

        [StringLength(50)]
        [Required]
        public string name { get; set; }

        [StringLength(50)]
        public string job { get; set; }

        [StringLength(100)]
        public string address { get; set; }

        [StringLength(80)]
        public string email { get; set; }

    }

    public class EmployeesContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=employees;Uid=root;Pwd=;",ServerVersion.AutoDetect("Server=localhost;Database=employees;Uid=root;Pwd=;")); // CONNECTION STRING
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<People>().HasData(
                new People() {id=1, name="Józsi",address="győr", job="C# fejlesztő",email="jozsi@cegem.hu" },
                new People() {id=2, name="Béla", address = "győr", job ="Adatbázis fejlesztő",email="bela@cegem.hu" }
                );
        }

        public DbSet<People> people { get; set; }
    }

    public partial class MainWindow : Window
    {
        EmployeesContext employeesContext = new EmployeesContext();
        public MainWindow()
        {
            InitializeComponent();

            employeesContext.people.Load(); // select * from people
            DG_people.ItemsSource = employeesContext.people.Local.ToObservableCollection();
        }
    }
}

// DATA BINDING
namespace wpf08_data_binding
{
    public class Ember 
    {
		private string _nev;

		public string nev
		{
			get { return _nev; }
			set { _nev = value; }
		}

		private int _eletkor;

		public int eletkor
		{
			get { return _eletkor; }
			set { _eletkor = value; }
		}

		private string _varos;

		public string varos
		{
			get { return _varos; }
			set { _varos = value; }
		}

		public Ember()
		{
			nev = "Józsi";
			eletkor = 30;
			varos = "Győr";
		}
	}

    public partial class MainWindow : Window
    {
        Ember ember = new Ember();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = ember;
        }

        private void BTN_modosit_Click(object sender, RoutedEventArgs e)
        {
            ember.nev = "Béla";
        }
    }
}

// INTERFACE
namespace wpf11_alakzatok_interface_2
{
    public interface IAlakzat
    {
        public double kerulet { get; }
        public double terulet { get; }
        public int oldalakSzama { get; }
        public string nev { get; }
    }
    
    public class Negyzet : IAlakzat
    {
        private double _oldal;

        public double oldal
        {
            get { return _oldal; }
            set { 
                if (value<=0) { throw new ArgumentException("Az oldal csak pozitív szám lehet!"); }
                _oldal = value; 
            }
        }
        public double kerulet { get { return 4 * oldal; } }

        public double terulet => oldal * oldal;

        public int oldalakSzama => 4;

        public string nev => "Négyzet";

        public Negyzet(double oldal)
        {
            this.oldal = oldal;
        }
    }

    public class Kor : IAlakzat
    {
        private double _sugar;

        public double sugar
        {
            get { return _sugar; }
            set {
                if (value <= 0) { throw new ArgumentException("A sugár csak pozitív szám lehet!"); }
                _sugar = value; 
            }
        }

        public double kerulet => 2 * sugar * Math.PI;

        public double terulet => sugar * sugar * Math.PI;

        public int oldalakSzama => 1;

        public string nev => "Kör";
        public Kor(double sugar)
        {
            this.sugar = sugar;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<IAlakzat> alakzatok = new List<IAlakzat>();

        public MainWindow()
        {
            InitializeComponent();
            TB_sugar.Focus();
        }

        /// <summary>
        /// Frissíti a listbox-ot.
        /// </summary>
        private void listaFrissites()
        {
            LBO_alakzatok.Items.Clear();
            foreach (var alakzat in alakzatok)
            {
                if (alakzat is Kor)
                {
                    LBO_alakzatok.Items.Add($"Kör, sugar: {((Kor)alakzat).sugar}");
                }
                else
                {
                    LBO_alakzatok.Items.Add($"Négyzet, oldal: {((Negyzet)alakzat).oldal}");
                }
            }
        }

        /// <summary>
        /// Az alakzatok listához adja az objektumot
        /// </summary>
        private void hozzaadAlakzat()
        {
            var melyik = ((ComboBoxItem)CBO_melyik.SelectedValue).Content.ToString();

            LB_hiba.Text = "";
            try
            {
                if (melyik == "Kör")
                {
                    if (!double.TryParse(TB_sugar.Text, out double s))
                    {
                        LB_hiba.Text = "Csak szám lehet!";
                    }
                    else
                    {
                        alakzatok.Add(new Kor(double.Parse(TB_sugar.Text)));
                    }
                    TB_sugar.Text = "";
                    TB_sugar.Focus();
                }
                else
                {
                    double oldal;
                    if (!double.TryParse(TB_oldal.Text, out oldal))
                    {
                        LB_hiba.Text = "Csak szám lehet!";
                    }
                    else
                    {
                        alakzatok.Add(new Negyzet(oldal));
                    }
                    TB_oldal.Text = "";
                    TB_oldal.Focus();
                }
            }
            catch (Exception ex)
            {

                LB_hiba.Text = ex.Message;
            }


            listaFrissites();
        }
        
        private void CBO_melyik_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var melyik = ((ComboBoxItem)CBO_melyik.SelectedValue).Content.ToString();

            if (melyik == null || SP_kor == null || SP_negyzet == null) return;

            if (melyik == "Kör")
            {
                SP_kor.Visibility = Visibility.Visible;
                SP_negyzet.Visibility = Visibility.Hidden;
                TB_sugar.Focus();
            }
            else
            {
                SP_kor.Visibility = Visibility.Collapsed;
                SP_negyzet.Visibility = Visibility.Visible;
                TB_oldal.Focus();
            }
        }

        private void Button_Hozzaad(object sender, RoutedEventArgs e)
        {
            hozzaadAlakzat();
        }


        private void LBO_alakzatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = LBO_alakzatok.SelectedIndex;
            if (index == -1)
            {
                LB_alakzat.Content = "";
                LB_kerulet.Content = "";
                LB_terulet.Content = "";
                LB_oldalakSzama.Content = "";
                return;
            }

            LB_alakzat.Content = $"Alakzat: {alakzatok[index].nev}";
            LB_kerulet.Content = $"Kerület: {alakzatok[index].kerulet}";
            LB_terulet.Content = $"Terület: {alakzatok[index].terulet}";
            LB_oldalakSzama.Content = $"Oldalak száma: {alakzatok[index].oldalakSzama}";
        }

        private void TB_sugar_KeyUp(object sender, KeyEventArgs e)
        {
            //Debug.WriteLine(e.Key);
            if (e.Key == Key.Enter)
            {
                hozzaadAlakzat();
            }
        }

        private void TB_oldal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                hozzaadAlakzat();
            }
        }
    }
}

// COMMAND BINDING
namespace Wpf19_CommandBinding
{
    public class SajatParancsok
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand(
            "Kilépés",
            "Exit",
            typeof(SajatParancsok),
            new InputGestureCollection() { new KeyGesture(Key.Q, ModifierKeys.Control)}
            );

        public static readonly RoutedUICommand Felkover = new RoutedUICommand(
           "Félkövér",
           "Felkover",
           typeof(SajatParancsok),
           new InputGestureCollection() { new KeyGesture(Key.B, ModifierKeys.Control) }
           );
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CMD_open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Szöveg fájl (*.txt)|*.txt|CSV fájl (*.csv)|*.csv|Minden fájl (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == true)
            {
                TB_szoveg.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void CMD_save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter= "Szöveg fájl (*.txt)|*.txt|CSV fájl (*.csv)|*.csv";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, TB_szoveg.Text);
            }
        }

        private void CMD_save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TB_szoveg != null)
            {
                if (string.IsNullOrEmpty(TB_szoveg.Text))
                {
                    e.CanExecute = false;
                } else
                {
                    e.CanExecute = true;
                }
            }
        }

        private void CMD_exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void CMD_felkover_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MI_felkover.IsChecked)
            {
                TB_szoveg.FontWeight = FontWeights.Bold;
            }else
            {
                TB_szoveg.FontWeight = FontWeights.Normal;
            }
        }

        private void CMD_felkover_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TB_szoveg != null)
            {
                if (string.IsNullOrEmpty(TB_szoveg.Text))
                {
                    e.CanExecute = false;
                }
                else
                {
                    e.CanExecute = true;
                }
            }
        }
    }
}

// BUTORBOLT SQL
// NuGet mysql.data
using MySql.Data.MySqlClient;
using Wpf21_butorbolt_mysql.Models;

namespace Wpf21_butorbolt_mysql
{
    public class AlapanyagModel
    {
        public int? id { get; set; }
        public string megnevezes { get; set; }

        public AlapanyagModel(MySqlDataReader reader)
        {
            this.id = Convert.ToInt32(reader["id"]);
            this.megnevezes = reader["megnevezes"].ToString();
        }
        public AlapanyagModel(int? id, string megnevezes)
        {
            this.id = id;
            this.megnevezes = megnevezes;
        }

        public static List<AlapanyagModel> select()
        {
            var lista = new List<AlapanyagModel>();
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                var sql = "SELECT * FROM alapanyag";
               
                using (var cmd = new MySqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new AlapanyagModel(reader));
                        }
                    }
                }
            }
            return lista;
        }
    }

    public class ButorModel
    {
        public int id { get; set; }
        public string megnevezes { get; set; }
        public int? ar { get; set; }
        public string szin { get; set; }
        public DateTime? szallitas { get; set; }
        public int alapanyag_id { get; set; }

        // extra mezők
        public string alapanyagNev { get; set; }

        public ButorModel() { }

        public ButorModel(MySqlDataReader reader)
        {
            this.id = Convert.ToInt32(reader["id"]);
            this.megnevezes = reader["megnevezes"].ToString();
            this.ar = reader["ar"] == DBNull.Value ? null : Convert.ToInt32(reader["ar"]);
            this.szin = reader["szin"].ToString();
            this.szallitas = reader["szallitas"] == DBNull.Value ? null : Convert.ToDateTime(reader["szallitas"]);
            this.alapanyag_id = Convert.ToInt32(reader["alapanyag_id"]);

            this.alapanyagNev = reader["alapanyagnev"].ToString();
        }

        public static List<ButorModel> select(int? alapanyagId, string megnevezes)
        {
            var lista = new List<ButorModel>();
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                var sql = "SELECT *,alapanyag.megnevezes AS alapanyagnev " +
                    "FROM butor " +
                    "INNER JOIN alapanyag ON butor.alapanyag_id = alapanyag.id " +
                    "WHERE 1=1 ";

                if (alapanyagId != null)
                {
                    sql += "AND butor.alapanyag_id = @alapanyagId ";
                }
                if (!string.IsNullOrEmpty(megnevezes))
                {
                    sql += "AND butor.megnevezes LIKE @megnevezes ";
                }
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@alapanyagId", alapanyagId);
                    cmd.Parameters.AddWithValue("@megnevezes", "%" + megnevezes + "%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ButorModel(reader));
                        }
                    }
                }
            }
            return lista;
        }

        public static int insert(ButorModel model)
        {
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                var sql = "INSERT INTO butor (megnevezes,ar,szin,szallitas,alapanyag_id) "+
                          "VALUES (@megnevezes,@ar,@szin,@szallitas,@alapanyag_id) ";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@megnevezes", model.megnevezes);
                    cmd.Parameters.AddWithValue("@ar", model.ar);
                    cmd.Parameters.AddWithValue("@szin", model.szin);
                    cmd.Parameters.AddWithValue("@szallitas", model.szallitas);
                    cmd.Parameters.AddWithValue("@alapanyag_id", model.alapanyag_id);
                    cmd.ExecuteNonQuery();
                    return (int)cmd.LastInsertedId;
                }
            }
        }

        public static void update(ButorModel model)
        {
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                var sql = "UPDATE butor SET megnevezes=@megnevezes, ar=@ar, szin=@szin, szallitas=@szallitas, alapanyag_id=@alapanyag_id  WHERE id=@id";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@megnevezes", model.megnevezes);
                    cmd.Parameters.AddWithValue("@ar", model.ar);
                    cmd.Parameters.AddWithValue("@szin", model.szin);
                    cmd.Parameters.AddWithValue("@szallitas", model.szallitas);
                    cmd.Parameters.AddWithValue("@alapanyag_id", model.alapanyag_id);
                    cmd.Parameters.AddWithValue("@id", model.id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void delete(ButorModel model)
        {
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                var sql = "DELETE FROM butor WHERE id=@id";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", model.id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ButorModel> butorok = new List<ButorModel>();
        List<AlapanyagModel> alapanyagok = new List<AlapanyagModel>();

        public MainWindow()
        {
            InitializeComponent();

            //string connectionString = "Server=localhost;Database=14a_butorbolt;Uid=root;Pwd=;";
            //MySqlConnection con = new MySqlConnection(connectionString);
            //MySqlCommand cmd = new MySqlCommand("select * from butor", con);
            //con.Open();
            //DataTable dt = new DataTable();
            //dt.Load(cmd.ExecuteReader());
            //con.Close();
            butorok = ButorModel.select(null,"");
            DG_adatok.ItemsSource = butorok;

            alapanyagok = AlapanyagModel.select();
            alapanyagok.Insert(0, new AlapanyagModel(null,"Összes alapanyag"));
            CBO_alapanyag.ItemsSource = alapanyagok;
            CBO_alapanyag.SelectedIndex = 0;
        }

        public partial class ButorReszletek : Window
    {
        List<AlapanyagModel> alapanyagok = new List<AlapanyagModel>();
        public ButorModel butor { get; private set; }

        public ButorReszletek(ButorModel model)
        {
            InitializeComponent();
            butor = new ButorModel();
            butor = model;
            this.DataContext = butor;

            alapanyagok = AlapanyagModel.select();
            CBO_alapanyagok.ItemsSource = alapanyagok;
        }

        private void BTN_ok_Click(object sender, RoutedEventArgs e)
        {
            if (butor.id == 0)
            {
                ButorModel.insert(butor);
            } else
            {
                ButorModel.update(butor);
            }
            DialogResult = true;
            Close();
        }

        private void BTN_megse_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

        private void BTN_kereses_Click(object sender, RoutedEventArgs e)
        {
            butorok = ButorModel.select((int?)CBO_alapanyag.SelectedValue, TB_megnevezes.Text);
            DG_adatok.ItemsSource = butorok;
        }

        private void BTN_uj_Click(object sender, RoutedEventArgs e)
        {
            var ablak = new ButorReszletek(new ButorModel());
            if ( ablak.ShowDialog() == true)
            {
                butorok = ButorModel.select(null, "");
                DG_adatok.ItemsSource = butorok;
            }
        }

        private void BTN_modosit_Click(object sender, RoutedEventArgs e)
        {
            if (DG_adatok.SelectedItem != null)
            {
                ButorModel modosit = (ButorModel)DG_adatok.SelectedItem;
                var ablak = new ButorReszletek(modosit);
                if (ablak.ShowDialog() == true)
                {
                    butorok = ButorModel.select(null, "");
                    DG_adatok.ItemsSource = butorok;
                }
            }
        }

        private void BTN_torol_Click(object sender, RoutedEventArgs e)
        {
            if (DG_adatok.SelectedItem != null)
            {
                ButorModel torol = (ButorModel)DG_adatok.SelectedItem;
                if (MessageBox.Show("Törölhető-e a kijelölt bútor? ("+torol.megnevezes+")", "Tölés", MessageBoxButton.YesNo ) == MessageBoxResult.Yes)
                {
                    ButorModel.delete(torol);
                    butorok = ButorModel.select(null, "");
                    DG_adatok.ItemsSource = butorok;
                }
            }
        }
    }
}