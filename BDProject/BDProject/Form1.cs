using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace BDProject
{
    public partial class Form1 : Form
    {
        private DateTime DefaultDate = new DateTime(1753, 1, 1);

        private SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
            TestDBConnection();
            StartUpComponents();
        }

        private void TestDBConnection()
        {
            conn = new SqlConnection("Data Source=DESKTOP-K89T2FB;Initial Catalog=AnimeDB;Integrated Security=True");

            try
            {
                conn.Open();
                ConnectionStatus.Text = "Connected";
            }
            catch (Exception e)
            {
                ConnectionStatus.Text = "Not Connected (Error in Console)";
                Console.WriteLine("Error: " + e.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        // general catch all function to fill dropdowns
        private void StartUpComponents()
        {
            updateGenres();
            updateUsers();
            updateAnimes();
            updateVA();
            updateStudio();
            updateStaffType();
        }

        private Dictionary<String, int> genreDictionary = new Dictionary<String, int>();

        private void updateGenres()
        {
            genreDictionary.Clear();
            AnimeDetailsSelectGenre.Items.Clear();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Genre", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    genreDictionary.Add(reader[1].ToString(), int.Parse(reader[0].ToString()));
                    AnimeDetailsSelectGenre.Items.Add(reader[1].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private Dictionary<String, int> userDict = new Dictionary<String, int>();

        private void updateUsers()
        {
            userDict.Clear();
            AnimeDetailsUpdateAs.Items.Clear();
            AnimeCreateAsSelect.Items.Clear();
            AnimeDetailsCommentAs.Items.Clear();
            StudioDetailsUpdateAs.Items.Clear();
            StudioCreateAs.Items.Clear();
            StaffDetailsUpdateAs.Items.Clear();
            StaffCreateAs.Items.Clear();
            CharacterUpdateAs.Items.Clear();
            CharacterCreateAs.Items.Clear();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Name FROM Users", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userDict.Add(reader[1].ToString(), int.Parse(reader[0].ToString()));
                    AnimeDetailsUpdateAs.Items.Add(reader[1].ToString());
                    AnimeCreateAsSelect.Items.Add(reader[1].ToString());
                    AnimeDetailsCommentAs.Items.Add(reader[1].ToString());
                    StudioDetailsUpdateAs.Items.Add(reader[1].ToString());
                    StudioCreateAs.Items.Add(reader[1].ToString());
                    StaffDetailsUpdateAs.Items.Add(reader[1].ToString());
                    StaffCreateAs.Items.Add(reader[1].ToString());
                    CharacterUpdateAs.Items.Add(reader[1].ToString());
                    CharacterCreateAs.Items.Add(reader[1].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }

        Dictionary<String, int> animeDict = new Dictionary<String, int>();
        private void updateAnimes()
        {
            animeDict.Clear();
            AnimeDetailsSelAnimeRel.Items.Clear();
            CharacterFilterFromAnime.Items.Clear();
            CharacterDetailsAnime.Items.Clear();
            CharacterCreateAnime.Items.Clear();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Name FROM Anime", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    animeDict.Add(reader[1].ToString(), int.Parse(reader[0].ToString()));
                    AnimeDetailsSelAnimeRel.Items.Add(reader[1].ToString());
                    CharacterFilterFromAnime.Items.Add(reader[1].ToString());
                    CharacterDetailsAnime.Items.Add(reader[1].ToString());
                    CharacterCreateAnime.Items.Add(reader[1].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }


        private Dictionary<String, int> vaDict = new Dictionary<String, int>();

        private void updateVA()
        {
            vaDict.Clear();
            // CharacterFilterVoicedby, CharacterDetailsVA, CharacterCreateVA
            CharacterFilterVoicedby.Items.Clear();
            CharacterDetailsVA.Items.Clear();
            CharacterCreateVA.Items.Clear();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Name FROM Staff WHERE Type = 'Voice Actor'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    vaDict.Add(reader[1].ToString(), int.Parse(reader[0].ToString()));
                    CharacterFilterVoicedby.Items.Add(reader[1].ToString());
                    CharacterDetailsVA.Items.Add(reader[1].ToString());
                    CharacterCreateVA.Items.Add(reader[1].ToString());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private Dictionary<String, int> studioDict = new Dictionary<String, int>();

        private void updateStudio()
        {
            studioDict.Clear();
            AnimeDetailsStudio.Items.Clear();
            AnimeCreateStudio.Items.Clear();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Name FROM Studio", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    studioDict.Add(reader[1].ToString(), int.Parse(reader[0].ToString()));
                    AnimeDetailsStudio.Items.Add(reader[1].ToString());
                    AnimeCreateStudio.Items.Add(reader[1].ToString());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void updateStaffType()
        {
            String[] staffTypeList = { "Voice Actor", "Director", "Producer", "Animator", "Musician", "Writer", "Other" };
             
            for (int i = 0; i < staffTypeList.Length; i++)
            {
                StaffCreateType.Items.Add(staffTypeList[i]);
                StaffDetailsType.Items.Add(staffTypeList[i]);
                StaffCreateType.Items.Add(staffTypeList[i]);
            }
        }
        // Anime

        // Anime List

        private int SelectedAnimeID = -1;
        private int lastSearchedAnimeID = -1;
        private String FilterAnimeSearch = null;
        private float FilterAnimeScore;
        private DateTime FilterAnimeStartDate;
        private DateTime FilterAnimeEndDate;
        private int FilterAnimeOffset;


        // individual anime
        private String animeName;
        private String animeAltName;
        private String animeSynopsis;
        private int animeID;
        private float animeScore;
        private DateTime animeStartDate;
        private DateTime animeEndDate;
        private int animeEpisodes;
        private String animeSeason;

        private void AnimeBtn_Click(object sender, EventArgs e)
        {
            animeListView.Items.Clear();
            AnimeTab.Visible = true;
            CharactersTab.Visible = false;
            StudioTab.Visible = false;
            StaffTab.Visible = false;
            UsersTab.Visible = false;

            resetAnimeVars();
            requestAnimeList();
        }

        private void AnimeFilterName_TextChanged(object sender, EventArgs e)
        {
            FilterAnimeSearch = AnimeFilterName.Text;
        }

        private void AnimeFilterScore_ValueChanged(object sender, EventArgs e)
        {
            FilterAnimeScore = (float)AnimeFilterScore.Value;
        }

        private void AnimeFilterAfterDate_ValueChanged(object sender, EventArgs e)
        {
            FilterAnimeStartDate = AnimeFilterAfterDate.Value;
        }

        private void AnimeFilterBeforeDate_ValueChanged(object sender, EventArgs e)
        {
            FilterAnimeEndDate = AnimeFilterBeforeDate.Value;
        }

        private void AnimeApplyFilterbtn_Click(object sender, EventArgs e)
        {
            animeListView.Items.Clear();
            requestAnimeList();
        }

        private void AnimeListPage_ValueChanged(object sender, EventArgs e)
        {
            FilterAnimeOffset = (int)AnimeListPage.Value;
        }

        private void ClearFilterButton_Click(object sender, EventArgs e)
        {
            resetAnimeVars();
        }

        private void animeListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
            {
                return;
            }
            Console.WriteLine("DEBUG: Selected Anime ID -> " + e.Item.Text);
            SelectedAnimeID = int.Parse(e.Item.Text);
        }

        private void resetAnimeVars()
        {
            // reset fields
            AnimeFilterName.Text = "";
            AnimeFilterScore.Value = 0.0m;
            // set day to today 
            AnimeFilterAfterDate.Value = DateTime.Now;
            AnimeFilterBeforeDate.Value = DateTime.Now;

            // reset vars
            FilterAnimeSearch = null;
            FilterAnimeScore = 0.0f;
            FilterAnimeStartDate = DefaultDate;
            FilterAnimeEndDate = DefaultDate;
            FilterAnimeOffset = 0;
        }

        private void requestAnimeList()
        {
            String command = $"EXEC FilterAnime @Name = {(FilterAnimeSearch == null ? "NULL" : "'" + FilterAnimeSearch + "'")}, @MinScore = {(FilterAnimeScore == 0.0 ? "NULL" : FilterAnimeScore.ToString())}, @MinDate = {(FilterAnimeStartDate == DefaultDate ? "NULL" : "'" + FilterAnimeStartDate.ToString("yyyy-MM-dd") + "'")}, @MaxDate = {(FilterAnimeEndDate == DefaultDate ? "NULL" : "'" + FilterAnimeEndDate.ToString("yyyy-MM-dd") + "'")}, @Offset = {FilterAnimeOffset * 20}";
            Console.WriteLine("DEBUG: Executing Command -> " + command);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    animeListView.Items.Add(reader[0].ToString());
                    for (int i = 1; i < reader.FieldCount; i++)
                    {
                        if (reader[i].ToString() == "")
                        {
                            animeListView.Items[animeListView.Items.Count - 1].SubItems.Add("N/A");
                        }
                        else
                        {
                            animeListView.Items[animeListView.Items.Count - 1].SubItems.Add(reader[i].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void requestSingleAnime()
        {
            if (SelectedAnimeID == -1 || SelectedAnimeID == lastSearchedAnimeID)
            {
                return;
            }
            resetAnimeDetails();

            try
            {
                conn.Open();
                // get anime details
                SqlCommand cmd = new SqlCommand("SELECT * FROM Anime WHERE ID = " + SelectedAnimeID, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                int studioID = -1;
                while (reader.Read())
                {
                    AnimeDetailsID.Text = reader["ID"].ToString();
                    AnimeDetailsName.Text = reader["Name"].ToString();
                    AnimeDetailsAltName.Text = reader["Alt_Name"].ToString();
                    AnimeDetailsSynopsis.Text = reader["Synopsis"].ToString();
                    AnimeDetailsScore.Text = reader["Score"].ToString();
                    AnimeDetailsAirDate.Text = reader["Aired_Date"].ToString();
                    AnimeDetailsFinishedDate.Text = reader["Finished_Date"].ToString();
                    AnimeDetailsEpisodes.Text = reader["Episodes"].ToString();
                    AnimeDetailsSeason.Text = reader["Season"].ToString();
                    studioID = int.Parse(reader["FK_Studio_ID"].ToString());
                }

                reader.Close();

                // get studio name
                cmd = new SqlCommand("SELECT Name FROM Studio WHERE ID = " + studioID, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AnimeDetailsStudio.Text = reader["Name"].ToString();
                }

                reader.Close();
                //get genres id
                cmd = new SqlCommand("EXEC GetAnimeGenres @AnimeID = " + SelectedAnimeID, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["Name"].ToString());
                    item.Tag = reader["ID"].ToString();
                    AnimeDetailsGenres.Items.Add(item);
                }

                reader.Close();
                // get related anime
                cmd = new SqlCommand("EXEC GetRelatedAnimes @AnimeID = " + SelectedAnimeID, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AnimeDetailsRelated.Items.Add(reader["FK_AnimeID2"].ToString());
                    AnimeDetailsRelated.Items[AnimeDetailsRelated.Items.Count - 1].SubItems.Add(reader["AnimeName"].ToString());
                    AnimeDetailsRelated.Items[AnimeDetailsRelated.Items.Count - 1].SubItems.Add(reader["Relation"].ToString());
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            lastSearchedAnimeID = SelectedAnimeID;
        }
        private void resetAnimeDetails()
        {
            return;
        }

        private void AnimeDetailsGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnimeDetailsGenres.SelectedItems.Count == 0)
            {
                return;
            }

            Console.WriteLine("DEBUG: Selected Genre ID -> " + AnimeDetailsGenres.SelectedItems[0].Tag.ToString());
        }

        private void AnimeTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnimeTab.SelectedIndex == 0)
            {
                animeListView.Items.Clear();
                resetAnimeVars();
                requestAnimeList();
            }
            else if (AnimeTab.SelectedIndex == 1)
            {
                requestSingleAnime();
            }
        }

        // Characters
        private void CharactersBtn_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = true;
            StudioTab.Visible = false;
            StaffTab.Visible = false;
            UsersTab.Visible = false;

        }

        // Studios
        private void StudiosBtn_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = false;
            StudioTab.Visible = true;
            StaffTab.Visible = false;
            UsersTab.Visible = false;
        }

        // Staff
        private void StaffBtn_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = false;
            StudioTab.Visible = false;
            StaffTab.Visible = true;
            UsersTab.Visible = false;
        }

        // Users
        private void UserBtn_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = false;
            StudioTab.Visible = false;
            StaffTab.Visible = false;
            UsersTab.Visible = true;
        }


    }
}
