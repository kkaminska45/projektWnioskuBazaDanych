using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace projektWnioskuBazaDanych

{
    public partial class Wniosek : Form
    {
        public Wniosek()
        {
            InitializeComponent();
        }

        public void StworzTabele()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"CREATE TABLE IF NOT EXISTS Wnioski (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ""Poznañ, dnia"" TEXT,
                    ""Numer albumu"" TEXT,
                    ""Nazwisko, imiê"" TEXT,
                    ""Semestr, Rok"" TEXT,
                    ""Kierunek, stopieñ studiów"" TEXT,
                    Przedmiot TEXT,
                    Punkty TEXT,
                    ""Prowadz¹cy przedmiot"" TEXT,
                    Uzasadnienie TEXT,
                    ""Podpis studenta"" TEXT,
                    ""Sk³ad komisji"" TEXT,
                    ""Data podjêcia decyzji"" TEXT,
                    ""Podpis pod decyzj¹"" TEXT
                );";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            StworzTabele();
        }

        private string connectionString = @"Data Source=Wnioski.db;Version=3;";

        private void ZapiszWniosek(string data1, string nrAlbumu, string imieNazwisko, string semestrRok, string kierunekStopien, string przedmiot, float punkty, string prowadzacy, string uzasadnienie, string podpisStudenta, string komisja, string dataDecyzji, string podpisKomisji)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO Wnioski (
                            ""Poznañ, dnia"",
                            ""Numer albumu"",
                            ""Nazwisko, imiê"",
                            ""Semestr, Rok"",
                            ""Kierunek, stopieñ studiów"",
                            Przedmiot,
                            Punkty,
                            ""Prowadz¹cy przedmiot"",
                            Uzasadnienie,
                            ""Podpis studenta"",
                            ""Sk³ad komisji"",
                            ""Data podjêcia decyzji"",
                            ""Podpis pod decyzj¹"")
                         VALUES (
                            @Data1,
                            @NrAlbumu,
                            @ImieNazwisko,
                            @SemestrRok,
                            @KierunekStopien,
                            @Przedmiot,
                            @Punkty,
                            @Prowadzacy,
                            @Uzasadnienie,
                            @PodpisStudenta,
                            @Komisja,
                            @DataDecyzji,
                            @PodpisKomisji);";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Data1", data1);
                    command.Parameters.AddWithValue("@NrAlbumu", nrAlbumu);
                    command.Parameters.AddWithValue("@ImieNazwisko", imieNazwisko);
                    command.Parameters.AddWithValue("@SemestrRok", semestrRok);
                    command.Parameters.AddWithValue("@KierunekStopien", kierunekStopien);
                    command.Parameters.AddWithValue("@Przedmiot", przedmiot);
                    command.Parameters.AddWithValue("@Punkty", punkty.ToString());
                    command.Parameters.AddWithValue("@Prowadzacy", prowadzacy);
                    command.Parameters.AddWithValue("@Uzasadnienie", uzasadnienie);
                    command.Parameters.AddWithValue("@PodpisStudenta", podpisStudenta);
                    command.Parameters.AddWithValue("@Komisja", komisja);
                    command.Parameters.AddWithValue("@DataDecyzji", dataDecyzji);
                    command.Parameters.AddWithValue("@PodpisKomisji", podpisKomisji);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void btnZapisz_Click_1(object sender, EventArgs e)
        {
            string data1 = textBox1Data1.Text;
            string nrAlbumu = textBox2NrAlbumu.Text;
            string imieNazwisko = textBox3NazwiskoImie.Text;
            string semestrRok = textBox4SemestrRok.Text;
            string kierunekStopien = textBox5KierunekStopien.Text;
            string przedmiot = textBox6Przedmiot.Text;
            float punkty = float.TryParse(textBox7Punkty.Text, out float wynikPunktow) ? wynikPunktow : 0;
            string prowadzacy = textBox8Prowadzacy.Text;
            string uzasadnienie = richTextBox1UzasadnienieWniosku.Text;
            string podpisStudenta = textBox9Data2Podpis.Text;
            string dataDecyzji = textBox13Data3.Text;
            string podpisKomisji = textBox14PodpisPieczatka.Text;

            string komisja = $"{textBox10SkladKomisji1.Text}, {textBox11SkladKomisji2.Text}, {textBox12SkladKomisji3.Text}";

            ZapiszWniosek(data1, nrAlbumu, imieNazwisko, semestrRok, kierunekStopien, przedmiot, punkty, prowadzacy, uzasadnienie, podpisStudenta, komisja, dataDecyzji, podpisKomisji);

            MessageBox.Show("Dane zosta³y zapisane!");
        }
        private void WypelnijPrzykladoweDane()
        {
            textBox1Data1.Text = "2025-05-11";
            textBox2NrAlbumu.Text = "123456";
            textBox3NazwiskoImie.Text = "Kowalski Jan";
            textBox4SemestrRok.Text = "Letni, 2024/2025";
            textBox5KierunekStopien.Text = "Informatyka, I stopieñ";
            textBox6Przedmiot.Text = "Programowanie obiektowe";
            textBox7Punkty.Text = "3.0";
            textBox8Prowadzacy.Text = "dr in¿. Nowak Anna";
            richTextBox1UzasadnienieWniosku.Text = "Proszê o zaliczenie przedmiotu z powodu choroby w trakcie semestru.";
            textBox9Data2Podpis.Text = "Jan Kowalski";

            textBox10SkladKomisji1.Text = "Prof. Kowalska";
            textBox11SkladKomisji2.Text = "Dr. Nowak";
            textBox12SkladKomisji3.Text = "Mgr. Zieliñski";

            textBox13Data3.Text = "2025-05-10";
            textBox14PodpisPieczatka.Text = "Podpis komisji";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WypelnijPrzykladoweDane();
        }

        private void btnOdczytaj_Click(object sender, EventArgs e)
        {
            Form2 oknoOdczytu = new Form2();
            oknoOdczytu.ShowDialog();
        }
    }
}
