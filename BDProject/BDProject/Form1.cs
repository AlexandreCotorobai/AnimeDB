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
using System.Runtime.Remoting.Messaging;

namespace BDProject
{
    public partial class Form1 : Form
    {
        private DateTime DefaultDate;
        private bool debug = true;
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
                if (debug)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
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
            AnimeCommentsRemoveAs.Items.Clear();
            UserRemoveAs.Items.Clear();
            StaffRemoveAs.Items.Clear();
            AnimeRemoveAs.Items.Clear();
            CharRemoveAs.Items.Clear();
            RemoveStudioAs.Items.Clear();
            UserSelectNewFriend.Items.Clear();
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
                    AnimeCommentsRemoveAs.Items.Add(reader[1].ToString());
                    UserRemoveAs.Items.Add(reader[1].ToString());
                    StaffRemoveAs.Items.Add(reader[1].ToString());
                    AnimeRemoveAs.Items.Add(reader[1].ToString());
                    CharRemoveAs.Items.Add(reader[1].ToString());
                    RemoveStudioAs.Items.Add(reader[1].ToString());
                    UserSelectNewFriend.Items.Add(reader[1].ToString());
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
            CharDetailsAddAnimeAppearsIn.Items.Clear();
            UserSelectNewAnimeRating.Items.Clear();
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
                    CharDetailsAddAnimeAppearsIn.Items.Add(reader[1].ToString());
                    UserSelectNewAnimeRating.Items.Add(reader[1].ToString());
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

            if (conn.State == ConnectionState.Open)
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
                StaffFilterType.Items.Add(staffTypeList[i]);
            }
        }
        // Anime

        // Anime List
        private int AnimeRemoveAsID = -1;
        private void AnimeRemoveAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnimeRemoveAsID = userDict[AnimeRemoveAs.SelectedItem.ToString()];

            if (debug)
            {
                Console.WriteLine("Anime Remove As ID: " + AnimeRemoveAsID);
            }
        }

        private void AnimeRemoveBtn_Click(object sender, EventArgs e)
        {
            if (SelectedAnimeID == -1 || AnimeRemoveAsID == -1)
            {
                AnimeRemoveStatus.Text = "Please select an anime and a user to remove it as";
                return;
            }

            String command = $"EXEC RemoveAnime @AnimeID = {SelectedAnimeID}, @UserID = {AnimeRemoveAsID}";
            if (debug)
            {
                Console.WriteLine(command);
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.ExecuteNonQuery();
                AnimeRemoveStatus.Text = "Anime Removed";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AnimeRemoveStatus.Text = "Error Removing Anime";
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            updateAnimes();
            AnimeRemoveAsID = -1;
            animeListView.Items.Clear();
            requestAnimeList();
        }

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
        private int animeStudioID;
        private DateTime animeStartDate;
        private DateTime animeEndDate;
        private int animeEpisodes;
        private int animeRemoveGenre;
        private int animeAddGenre;
        private int animeUpdateAs;
        private int animeAddRelatedAnime;
        private String animeAddRelation;
        private int animeRelatedAnime;


        // create anime vars
        private String animeCreateName;
        private String animeCreateAltName;
        private String animeCreateSynopsis;
        private int animeCreateStudioID;
        private DateTime animeCreateStartDate;
        private DateTime animeCreateEndDate;
        private int animeCreateEpisodes;
        private int animeCreateAs;

        // comment vars
        private int animeCommentAnimeID;
        private String animeCommentText;
        private int animeCommentUserID;
        private int animeCommentRemoveAs;
        private int lastSearchedAnimeIDComments = -1;

        private void AnimeBtn_Click(object sender, EventArgs e)
        {
            animeListView.Items.Clear();
            AnimeTab.Visible = true;
            CharactersTab.Visible = false;
            StudioTab.Visible = false;
            StaffTab.Visible = false;
            UsersTab.Visible = false;

            AnimeListPage.Value = 1;
            FilterAnimeOffset = 1;
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
            animeListView.Items.Clear();
            requestAnimeList();
        }

        private void ClearFilterButton_Click(object sender, EventArgs e)
        {
            resetAnimeVars();
            animeListView.Items.Clear();
            requestAnimeList();

        }

        private void animeListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
            {
                return;
            }
            if (debug)
            {
                Console.WriteLine("DEBUG: Selected Anime ID -> " + e.Item.Text);
            }
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

        }

        private void requestAnimeList()
        {
            String command = $"EXEC FilterAnime @Name = {(FilterAnimeSearch == null ? "NULL" : "'" + FilterAnimeSearch + "'")}, @MinScore = {(FilterAnimeScore == 0.0 ? "NULL" : FilterAnimeScore.ToString())}, @MinDate = {(FilterAnimeStartDate == DefaultDate ? "NULL" : "'" + FilterAnimeStartDate.ToString("yyyy-MM-dd") + "'")}, @MaxDate = {(FilterAnimeEndDate == DefaultDate ? "NULL" : "'" + FilterAnimeEndDate.ToString("yyyy-MM-dd") + "'")}, @Offset = {(FilterAnimeOffset - 1) * 20}";
            if (debug)
            {
                Console.WriteLine("DEBUG: Executing Command -> " + command);
            }
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

        private void requestSingleAnimeGenres()
        {
            SqlCommand cmd = new SqlCommand("EXEC GetAnimeGenres @AnimeID = " + SelectedAnimeID, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["Name"].ToString());
                item.Tag = reader["ID"].ToString();
                AnimeDetailsGenres.Items.Add(item);
            }

            reader.Close();
        }

        private void requestSingleAnimeRelations()
        {
            SqlCommand cmd = new SqlCommand("EXEC GetRelatedAnimes @AnimeID = " + SelectedAnimeID, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AnimeDetailsRelated.Items.Add(reader["FK_AnimeID2"].ToString());
                AnimeDetailsRelated.Items[AnimeDetailsRelated.Items.Count - 1].SubItems.Add(reader["AnimeName"].ToString());
                AnimeDetailsRelated.Items[AnimeDetailsRelated.Items.Count - 1].SubItems.Add(reader["Relation"].ToString());
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
                SqlCommand cmd = new SqlCommand("EXEC GetAnime @AnimeID = " + SelectedAnimeID, conn);
                SqlDataReader reader = cmd.ExecuteReader();
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
                    AnimeDetailsStudio.Text = reader["StudioName"].ToString();
                }

                reader.Close();
                //get genres id
                requestSingleAnimeGenres();
                // get related anime
                requestSingleAnimeRelations();

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
            AnimeDetailsID.Text = "";
            AnimeDetailsName.Text = "";
            AnimeDetailsAltName.Text = "";
            AnimeDetailsSynopsis.Text = "";
            AnimeDetailsScore.Text = "";
            AnimeDetailsAirDate.Text = "";
            AnimeDetailsFinishedDate.Text = "";
            AnimeDetailsEpisodes.Text = "";
            AnimeDetailsSeason.Text = "";
            AnimeDetailsStudio.Text = "";
            AnimeDetailsGenres.Items.Clear();
            AnimeDetailsRelated.Items.Clear();
        }

        private void AnimeTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnimeTab.SelectedIndex == 0)
            {
                animeListView.Items.Clear();
                AnimeListPage.Value = 1;
                FilterAnimeOffset = 1;
                resetAnimeVars();
                requestAnimeList();
            }
            else if (AnimeTab.SelectedIndex == 1)
            {
                requestSingleAnime();
            }
            else if (AnimeTab.SelectedIndex == 2)
            {
                resetAnimeCreate();
            }
            else if (AnimeTab.SelectedIndex == 3)
            {
                requestAnimeComments();
            }
        }

        private void AnimeDetailsName_TextChanged(object sender, EventArgs e)
        {
            animeName = AnimeDetailsName.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Name -> " + animeName);
            }

        }

        private void AnimeDetailsEpisodes_ValueChanged(object sender, EventArgs e)
        {
            animeEpisodes = (int)AnimeDetailsEpisodes.Value;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Episodes -> " + animeEpisodes);
            }
        }

        private void AnimeDetailsAltName_TextChanged(object sender, EventArgs e)
        {
            animeAltName = AnimeDetailsAltName.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Alt Name -> " + animeAltName);
            }
        }

        private void AnimeDetailsStudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            animeStudioID = studioDict[AnimeDetailsStudio.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Studio ID -> " + animeStudioID);
            }
        }


        private void AnimeDetailsAirDate_ValueChanged(object sender, EventArgs e)
        {
            animeStartDate = AnimeDetailsAirDate.Value;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Air Date -> " + animeStartDate);
            }
        }

        private void AnimeDetailsFinishedDate_ValueChanged(object sender, EventArgs e)
        {
            animeEndDate = AnimeDetailsFinishedDate.Value;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Finished Date -> " + animeEndDate);
            }
        }

        private void AnimeDetailsSelectGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            animeAddGenre = genreDictionary[AnimeDetailsSelectGenre.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Genre ID -> " + animeAddGenre);
            }
        }

        private void AnimeDetailsGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AnimeDetailsGenres.SelectedItems.Count == 0)
            {
                return;
            }

            animeRemoveGenre = int.Parse(AnimeDetailsGenres.SelectedItems[0].Tag.ToString());
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Remove Genre -> " + animeRemoveGenre);
            }
        }

        private void AnimeDetailsUpdateAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            animeUpdateAs = userDict[AnimeDetailsUpdateAs.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Update As -> " + animeUpdateAs);
            }
        }

        private void AnimeDetailsSynopsis_TextChanged(object sender, EventArgs e)
        {
            animeSynopsis = AnimeDetailsSynopsis.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Synopsis -> " + animeSynopsis);
            }
        }
        private void AnimeDetailsSelAnimeRel_SelectedIndexChanged(object sender, EventArgs e)
        {
            animeAddRelatedAnime = animeDict[AnimeDetailsSelAnimeRel.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Related Anime -> " + animeAddRelatedAnime);
            }
        }

        private void AnimeDetailsRelation_TextChanged(object sender, EventArgs e)
        {
            animeAddRelation = AnimeDetailsRelation.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Relation -> " + animeAddRelation);
            }
        }

        private void AnimeDetailsRelated_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
            {
                return;
            }
            animeRelatedAnime = int.Parse(e.Item.SubItems[0].Text);
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Related Anime -> " + animeRelatedAnime);
            }
        }

        private void animeDetailsAddGenre_Click(object sender, EventArgs e)
        {
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Add Genre -> " + animeAddGenre);
                Console.WriteLine("DEBUG: Anime ID -> " + SelectedAnimeID);
            }
            if (animeAddGenre == 0 || SelectedAnimeID == -1 || animeUpdateAs == 0)
            {
                AnimeUpdateStatus.Text = "Please Select an User and a Genre";
                return;
            }

            String command = "EXEC AddAnimeGenre @AnimeID = " + SelectedAnimeID + ", @GenreID = " + animeAddGenre + ", @UserID = " + animeUpdateAs;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Add Genre Command -> " + command);
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.ExecuteNonQuery();
                AnimeUpdateStatus.Text = "Genre Added";
                AnimeDetailsGenres.Items.Clear();
                requestSingleAnimeGenres();
            }
            catch (Exception ex)
            {
                AnimeUpdateStatus.Text = "Error Adding Genre";
                Console.WriteLine(ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        private void AnimeDetailsUpdateButton_Click(object sender, EventArgs e)
        {
            if (animeUpdateAs == 0 || SelectedAnimeID == -1)
            {
                AnimeUpdateStatus.Text = "Please Select an User";
                return;
            }

            String command = "EXEC UpdateAnime @AnimeID = " + SelectedAnimeID + ", @UserID = " + animeUpdateAs + ", @Name = '" + animeName + "', @Alt_Name = '" + animeAltName + "', @Synopsis = '" + animeSynopsis + "', @Episodes = " + animeEpisodes + ", @AiredDate = '" + animeStartDate.ToString("yyyy-MM-dd") + "', @FinishedDate = '" + animeEndDate.ToString("yyyy-MM-dd") + "', @StudioID = " + animeStudioID;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Update Command -> " + command);
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.ExecuteNonQuery();
                AnimeUpdateStatus.Text = "Anime Updated";
            }
            catch (SqlException ex)
            {
                AnimeUpdateStatus.Text = "Error Updating Anime";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            updateAnimes();
        }
        private void animeDetailsRemoveGenre_Click(object sender, EventArgs e)
        {
            if (animeRemoveGenre == 0 || SelectedAnimeID == -1 || animeUpdateAs == 0)
            {
                AnimeUpdateStatus.Text = "Please Select an User and a Genre";
                return;
            }

            String command = "EXEC RemoveAnimeGenre @AnimeID = " + SelectedAnimeID + ", @GenreID = " + animeRemoveGenre + ", @UserID = " + animeUpdateAs;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Remove Genre Command -> " + command);
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.ExecuteNonQuery();
                AnimeUpdateStatus.Text = "Genre Removed";
                AnimeDetailsGenres.Items.Clear();
                requestSingleAnimeGenres();
            }
            catch (SqlException ex)
            {
                AnimeUpdateStatus.Text = "Error Removing Genre";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void AnimeRemoveRelBtn_Click(object sender, EventArgs e)
        {
            if (SelectedAnimeID == -1 || animeRelatedAnime == 0 || animeUpdateAs == 0)
            {
                AnimeUpdateStatus.Text = "Please Select an User and a Related Anime";
                return;
            }

            String command = "EXEC RemoveAnimeRelation @AnimeID = " + SelectedAnimeID + ", @RelatedAnimeID = " + animeRelatedAnime + ", @UserID = " + animeUpdateAs;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Remove Relation Command -> " + command);
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.ExecuteNonQuery();
                AnimeUpdateStatus.Text = "Relation Removed";
                AnimeDetailsRelated.Items.Clear();
                requestSingleAnimeRelations();
            }
            catch (Exception ex)
            {
                AnimeUpdateStatus.Text = "Error Removing Relation";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void AnimeAddRelationBtn_Click(object sender, EventArgs e)
        {
            if (SelectedAnimeID == -1 || animeUpdateAs == 0 || animeAddRelation == "")
            {
                AnimeUpdateStatus.Text = "Please Select an User, a Related Anime and a Relation";
                return;
            }

            String command = "EXEC AddAnimeRelation @AnimeID = " + SelectedAnimeID + ", @RelatedAnimeID = " + animeAddRelatedAnime + ", @RelationType = '" + animeAddRelation + "', @UserID = " + animeUpdateAs;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Add Relation Command -> " + command);
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.ExecuteNonQuery();
                AnimeUpdateStatus.Text = "Relation Added";
                AnimeDetailsRelated.Items.Clear();
                requestSingleAnimeRelations();
            }
            catch (SqlException ex)
            {
                AnimeUpdateStatus.Text = "Error Adding Relation";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void AnimeCreateName_TextChanged(object sender, EventArgs e)
        {
            animeCreateName = AnimeCreateName.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Create Name -> " + animeCreateName);
            }
        }

        private void AnimeCreateAltName_TextChanged(object sender, EventArgs e)
        {
            animeCreateAltName = AnimeCreateAltName.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Create Alt Name -> " + animeCreateAltName);
            }
        }

        private void AnimeCreateStudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            animeCreateStudioID = studioDict[AnimeDetailsStudio.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Create Studio ID -> " + animeCreateStudioID);
            }
        }

        private void AnimeCreateEp_ValueChanged(object sender, EventArgs e)
        {
            animeCreateEpisodes = (int)AnimeCreateEp.Value;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Create Episodes -> " + animeCreateEpisodes);
            }
        }

        private void AnimeCreateSynopsis_TextChanged(object sender, EventArgs e)
        {
            animeCreateSynopsis = AnimeCreateSynopsis.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Create Synopsis -> " + animeCreateSynopsis);
            }
        }

        private void AnimeCreateFinishDate_ValueChanged(object sender, EventArgs e)
        {
            animeCreateEndDate = AnimeCreateFinishDate.Value;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Create End Date -> " + animeCreateEndDate);
            }
        }

        private void AnimeCreateAirDate_ValueChanged(object sender, EventArgs e)
        {
            animeCreateStartDate = AnimeCreateAirDate.Value;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Create Start Date -> " + animeCreateStartDate);
            }
        }

        private void AnimeCreateAsSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            animeCreateAs = userDict[AnimeCreateAsSelect.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Create As -> " + animeCreateAs);
            }
        }


        private void AnimeCreateButton_Click(object sender, EventArgs e)
        {
            if (animeCreateName == "" || animeCreateAltName == "" || animeCreateStudioID == 0 || animeCreateEpisodes == 0 || animeCreateSynopsis == "" || animeCreateEndDate == null || animeCreateStartDate == null || animeCreateAs == 0)
            {
                AnimeCreateStatus.Text = "Please Fill All Fields";
                return;
            }

            String command = "EXEC CreateAnime @Name = '" + animeCreateName + "', @Alt_Name = '" + animeCreateAltName + "', @StudioID = " + animeCreateStudioID + ", @Episodes = " + animeCreateEpisodes + ", @Synopsis = '" + animeCreateSynopsis + "', @FinishedDate = '" + animeCreateEndDate.ToString("yyyy-MM-dd") + "', @AiredDate = '" + animeCreateStartDate.ToString("yyyy-MM-dd") + "', @UserID = " + animeCreateAs;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Create Command -> " + command);
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.ExecuteNonQuery();
                AnimeCreateStatus.Text = "Anime Created";
                resetAnimeCreate();
            }
            catch (SqlException ex)
            {
                AnimeCreateStatus.Text = "Error Creating Anime";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            updateAnimes();
        }
        private void resetAnimeCreate()
        {
            AnimeCreateName.Text = "";
            AnimeCreateAltName.Text = "";
            AnimeCreateEp.Value = 0;
            AnimeCreateSynopsis.Text = "";
            AnimeCreateFinishDate.Value = DateTime.Now;
            AnimeCreateAirDate.Value = DateTime.Now;
            AnimeCreateAsSelect.SelectedIndex = 0;
            AnimeDetailsStudio.SelectedIndex = 0;
        }
        private void resetComments()
        {
            AnimeDetailsCommentTxt.Text = "";
            AnimeDetailsCommentAs.SelectedIndex = 0;
            AnimeCommentsRemoveAs.SelectedIndex = 0;
            resetCommentList();
        }

        private void resetCommentList()
        {
            AnimeDetailsComments.Items.Clear();
        }

        private void updateCommentList()
        {
            String command = "EXEC GetComments @AnimeID = " + SelectedAnimeID;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Comments Command -> " + command);
            }
            SqlCommand cmd = new SqlCommand(command, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AnimeDetailsComments.Items.Add(reader[0].ToString());
                for (int i = 1; i < reader.FieldCount; i++)
                {
                    AnimeDetailsComments.Items[AnimeDetailsComments.Items.Count - 1].SubItems.Add(reader[i].ToString());
                }
            }
        }
        private void requestAnimeComments()
        {
            if (SelectedAnimeID == -1 || lastSearchedAnimeIDComments == SelectedAnimeID)
            {
                return;
            }
            resetComments();

            try
            {
                conn.Open();
                updateCommentList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            lastSearchedAnimeIDComments = SelectedAnimeID;
        }

        private void AnimeDetailsCommentTxt_TextChanged(object sender, EventArgs e)
        {
            animeCommentText = AnimeDetailsCommentTxt.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Comment Text -> " + animeCommentText);
            }
        }

        private void AnimeDetailsCommentAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            animeCommentUserID = userDict[AnimeDetailsCommentAs.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Comment User ID -> " + animeCommentUserID);
            }
        }

        private void AnimeDetailsAddComment_Click(object sender, EventArgs e)
        {
            if (SelectedAnimeID == 0 || animeCommentText == "" || animeCommentUserID == 0)
            {
                AnimeDetailsCommentStatus.Text = "Please Fill All Fields";
                return;
            }

            String command = "EXEC CreateComment @AnimeID = " + SelectedAnimeID + ", @Comment = '" + animeCommentText + "', @UserID = " + animeCommentUserID;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Comment Command -> " + command);
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.ExecuteNonQuery();
                AnimeDetailsCommentStatus.Text = "Comment Added";
                resetCommentList();
                updateCommentList();
            }
            catch (Exception ex)
            {
                AnimeDetailsCommentStatus.Text = "Error Adding Comment";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }


        }

        private void AnimeCommentsRemoveAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            animeCommentRemoveAs = userDict[AnimeCommentsRemoveAs.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Comment Remove As -> " + animeCommentRemoveAs);
            }
        }
        private void AnimeDetailsComments_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
            {
                animeCommentAnimeID = 0;
                return;
            }
            animeCommentAnimeID = int.Parse(e.Item.Text);
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Comment Anime ID -> " + animeCommentAnimeID);
            }

        }
        private void RemoveCommentBtn_Click(object sender, EventArgs e)
        {
            if (animeCommentAnimeID == 0 || animeCommentRemoveAs == 0)
            {
                AnimeDetailsCommentStatus.Text = "Please Fill All Fields";
                return;
            }

            String command = "EXEC RemoveComment @CommentID = " + animeCommentAnimeID + ", @UserID = " + animeCommentRemoveAs;
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Comment Remove Command -> " + command);
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.ExecuteNonQuery();
                AnimeDetailsCommentStatus.Text = "Comment Removed";
                resetCommentList();
                updateCommentList();
            }
            catch (Exception ex)
            {
                AnimeDetailsCommentStatus.Text = "Error Removing Comment";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        // --------------------------------------------------------------------------------------------------- //

        // Characters
        private int CharacterRemoveAs = -1;

        private void CharRemoveAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            CharacterRemoveAs = userDict[CharRemoveAs.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Remove As -> " + CharacterRemoveAs);
            }
        }

        private void CharRemoveBtn_Click(object sender, EventArgs e)
        {
            if (SelectedCharacterID == -1 || CharacterRemoveAs == -1)
            {
                CharRemoveStatus.Text = "Please Fill All Fields";
                return;
            }

            String command = "EXEC RemoveCharacter @CharacterID = " + SelectedCharacterID + ", @UserID = " + CharacterRemoveAs;
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Remove Command -> " + command);
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.ExecuteNonQuery();
                CharRemoveStatus.Text = "Character Removed";
            }
            catch (Exception ex)
            {
                CharRemoveStatus.Text = "Error Removing Character";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            CharactersList.Items.Clear();
            requestCharList();
            CharacterRemoveAs = -1;
        }
        private int SelectedCharacterID = -1;
        private int lastSearchedCharacterID = -1;
        private int FilterCharAnimeID = -1;
        private String FilterCharName = null;
        private int FilterCharVoiceActorID = -1;
        private int FilterCharOffset = 0;

        // character details
        private String characterName = null;
        private String characterDescription = null;
        private int characterVAID = -1;
        private int characterRemoveAnimeID = -1;
        private int characterAddAnimeID = -1;
        private int characterUpdateAs = -1;

        // character create
        private String characterCreateName = null;
        private String characterCreateDescription = null;
        private int characterCreateVAID = -1;
        private int characterCreateAs = -1;
        private void CharactersBtn_Click(object sender, EventArgs e)
        {
            CharactersList.Items.Clear();
            AnimeTab.Visible = false;
            CharactersTab.Visible = true;
            StudioTab.Visible = false;
            StaffTab.Visible = false;
            UsersTab.Visible = false;

            CharacterListPage.Value = 1;
            FilterCharOffset = 1;
            resetCharVar();
            requestCharList();
        }
        private void CharactersTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CharactersTab.SelectedIndex == 0)
            {
                CharactersList.Items.Clear();
                CharacterListPage.Value = 1;
                FilterCharOffset = 1;
                resetCharVar();
                requestCharList();
            } else if (CharactersTab.SelectedIndex == 1)
            {
                requestSingleChar();
            } else if (CharactersTab.SelectedIndex == 2)
            {
                resetCharCreate();
            }
        }
        private void resetCharCreate()
        {
            CharacterCreateName.Text = "";
            CharacterCreateDescription.Text = "";
            CharacterCreateVA.SelectedIndex = 0;
            CharacterCreateAs.SelectedIndex = 0;
            CharCreateStatus.Text = "";
        }
        private void resetCharVar()
        {
            // reset fields
            CharacterNameFilter.Text = "";
            CharacterFilterFromAnime.SelectedIndex = 0;
            CharacterFilterVoicedby.SelectedIndex = 0;
            // reset vars
            FilterCharAnimeID = -1;
            FilterCharName = null;
            FilterCharVoiceActorID = -1;

        }

        private void requestCharList()
        {
            String command = $"EXEC FilterCharacter @AnimeID = {(FilterCharAnimeID == -1 ? "NULL" : FilterCharAnimeID.ToString())}, @Name = {(FilterCharName == null ? "NULL" : "'" + FilterCharName + "'")}, @VAID = {(FilterCharVoiceActorID == -1 ? "NULL" : FilterCharVoiceActorID.ToString())}, @Offset = {(FilterCharOffset - 1) * 20}";
            if (debug)
            {
                Console.WriteLine("DEBUG: Character List Command -> " + command);
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CharactersList.Items.Add(reader["ID"].ToString());
                    CharactersList.Items[CharactersList.Items.Count - 1].SubItems.Add(reader["Name"].ToString());
                    if (reader["AnimeName"].ToString() == "")
                    {
                        CharactersList.Items[CharactersList.Items.Count - 1].SubItems.Add("N/A");
                    }
                    else
                    {
                        CharactersList.Items[CharactersList.Items.Count - 1].SubItems.Add(reader["AnimeName"].ToString());
                    }
                    CharactersList.Items[CharactersList.Items.Count - 1].SubItems.Add(reader["VA"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void CharactersList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
            {
                SelectedCharacterID = -1;
                return;
            }
            if (debug)
            {
                Console.WriteLine("DEBUG: Selected Character ID -> " + e.Item.Text);
            }
            SelectedCharacterID = int.Parse(e.Item.Text);
        }

        private void CharacterNameFilter_TextChanged(object sender, EventArgs e)
        {
            FilterCharName = CharacterNameFilter.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Filter Name -> " + FilterCharName);
            }
        }

        private void CharacterFilterFromAnime_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterCharAnimeID = animeDict[CharacterFilterFromAnime.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Filter Anime ID -> " + FilterCharAnimeID);
            }
        }

        private void CharacterFilterVoicedby_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterCharVoiceActorID = vaDict[CharacterFilterVoicedby.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Filter VA ID -> " + FilterCharVoiceActorID);
            }
        }

        private void CharacterClearFilterBtn_Click(object sender, EventArgs e)
        {
            resetCharVar();
            CharactersList.Items.Clear();
            requestCharList();
        }

        private void CharacterApplyFilterBtn_Click(object sender, EventArgs e)
        {
            CharactersList.Items.Clear();
            requestCharList();
        }

        private void CharacterListPage_ValueChanged(object sender, EventArgs e)
        {
            FilterCharOffset = (int)CharacterListPage.Value;
            CharactersList.Items.Clear();
            requestCharList();
        }

        private void resetCharDetails()
        {
            CharDetailsAppearsInlst.Items.Clear();
            CharacterDetailsDescription.Text = "";
            CharacterDetailsID.Text = "";
            CharacterDetailsName.Text = "";
            CharacterDetailsVA.Text = "";
        }

        private void getAnimeAppearsIn()
        {
            SqlCommand cmd = new SqlCommand($"EXEC GetCharacterAnimes @CharacterID = {SelectedCharacterID}", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["AnimeName"].ToString());
                item.Tag = reader["AnimeID"].ToString();
                CharDetailsAppearsInlst.Items.Add(item);
            }
            reader.Close();
        }
        private void requestSingleChar()
        {
            if (SelectedCharacterID == -1 || SelectedCharacterID == lastSearchedCharacterID)
            {
                return;
            }

            resetCharDetails();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"EXEC GetCharacter @CharacterID = {SelectedCharacterID}", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CharacterDetailsID.Text = reader["ID"].ToString();
                    CharacterDetailsName.Text = reader["Name"].ToString();
                    CharacterDetailsDescription.Text = reader["Description"].ToString();
                    CharacterDetailsVA.Text = reader["VA"].ToString();
                }

                // get anime appears in
                reader.Close();
                getAnimeAppearsIn();

                lastSearchedCharacterID = SelectedCharacterID;
            }

            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void CharacterDetailsDescription_TextChanged(object sender, EventArgs e)
        {
            characterDescription = CharacterDetailsDescription.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Description -> " + characterDescription);
            }
        }

        private void CharacterDetailsName_TextChanged(object sender, EventArgs e)
        {
            characterName = CharacterDetailsName.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Name -> " + characterName);
            }

        }

        private void CharacterDetailsVA_SelectedIndexChanged(object sender, EventArgs e)
        {
            characterVAID = vaDict[CharacterDetailsVA.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Character VA -> " + characterVAID);
            }

        }

        private void CharacterUpdateAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            characterUpdateAs = userDict[CharacterUpdateAs.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Update As -> " + characterUpdateAs);
            }
        }

        private void CharDetailsAppearsInlst_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
            {
                characterRemoveAnimeID = -1;
                return;
            }

            characterRemoveAnimeID = int.Parse(e.Item.Tag.ToString());

            if (debug)
            {
                Console.WriteLine("DEBUG: Character Remove Anime ID -> " + characterRemoveAnimeID);
            }
        }

        private void CharDetailsAddAnimeAppearsIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CharDetailsAddAnimeAppearsIn.SelectedIndex == -1)
            {
                characterAddAnimeID = -1;
                return;
            }

            characterAddAnimeID = animeDict[CharDetailsAddAnimeAppearsIn.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Add Anime ID -> " + characterAddAnimeID);
            }
        }

        private void CharacterUpdateBtn_Click(object sender, EventArgs e)
        {
            if (characterUpdateAs == -1 || SelectedCharacterID == -1)
            {
                CharDetailStatus.Text = "Please select a user to update as";
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"EXEC UpdateCharacter @CharacterID = {SelectedCharacterID}, @Name = '{characterName}', @Description = '{characterDescription}', @VoiceActorID = {characterVAID}, @UserID = {characterUpdateAs}", conn);
                if (debug)
                {
                    Console.WriteLine("DEBUG: Update Character Query -> " + cmd.CommandText);
                }
                cmd.ExecuteNonQuery();
                CharDetailStatus.Text = "Character Updated";
            }
            catch (Exception ex)
            {
                CharDetailStatus.Text = "Error Updating Character";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void CharDetailsAddAppearsInBtn_Click(object sender, EventArgs e)
        {
            if (characterAddAnimeID == -1 || SelectedCharacterID == -1 || characterUpdateAs == -1)
            {
                CharDetailStatus.Text = "Please select an anime to add and a User to update as";
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"EXEC AddCharacterAppearsIn @CharacterID = {SelectedCharacterID}, @AnimeID = {characterAddAnimeID}, @UserID = {characterUpdateAs}", conn);
                if (debug)
                {
                    Console.WriteLine("DEBUG: Add Character Anime Query -> " + cmd.CommandText);
                }
                cmd.ExecuteNonQuery();
                CharDetailStatus.Text = "Anime Added";
                CharDetailsAppearsInlst.Items.Clear();
                getAnimeAppearsIn();
            }
            catch (Exception ex)
            {
                CharDetailStatus.Text = "Error Adding Anime";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void CharDetailsRemoveAppearsInBtn_Click(object sender, EventArgs e)
        {
            if (characterRemoveAnimeID == -1 || SelectedCharacterID == -1 || characterUpdateAs == -1)
            {
                CharDetailStatus.Text = "Please select an anime to remove and a User to update as";
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"EXEC RemoveCharacterAppearsIn @CharacterID = {SelectedCharacterID}, @AnimeID = {characterRemoveAnimeID}, @UserID = {characterUpdateAs}", conn);
                if (debug)
                {
                    Console.WriteLine("DEBUG: Remove Character Anime Query -> " + cmd.CommandText);
                }
                cmd.ExecuteNonQuery();
                CharDetailStatus.Text = "Anime Removed";
                CharDetailsAppearsInlst.Items.Clear();
                getAnimeAppearsIn();
            }
            catch (Exception ex)
            {
                CharDetailStatus.Text = "Error Removing Anime";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void CharacterCreateName_TextChanged(object sender, EventArgs e)
        {
            characterCreateName = CharacterCreateName.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Create Name -> " + characterCreateName);
            }
        }

        private void CharacterCreateDescription_TextChanged(object sender, EventArgs e)
        {
            characterCreateDescription = CharacterCreateDescription.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Create Description -> " + characterCreateDescription);
            }
        }

        private void CharacterCreateVA_SelectedIndexChanged(object sender, EventArgs e)
        {
            characterCreateVAID = vaDict[CharacterCreateVA.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Create VA -> " + characterCreateVAID);
            }
        }


        private void CharacterCreateAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            characterCreateAs = userDict[CharacterCreateAs.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Character Create As -> " + characterCreateAs);
            }
        }

        private void CharacterCreateEntry_Click(object sender, EventArgs e)
        {
            if (characterCreateName == "" || characterCreateDescription == "" || characterCreateVAID == -1 || characterCreateAs == -1)
            {
                CharCreateStatus.Text = "Please fill out all fields and choose a user to create";
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"EXEC CreateCharacter @Name = '{characterCreateName}', @Description = '{characterCreateDescription}', @VoiceActorID = {characterCreateVAID}, @UserID = {characterCreateAs}", conn);
                if (debug)
                {
                    Console.WriteLine("DEBUG: Create Character Query -> " + cmd.CommandText);
                }
                cmd.ExecuteNonQuery();
                CharCreateStatus.Text = "Character Created";
                resetCharCreate();
            }
            catch (Exception ex)
            {
                CharCreateStatus.Text = "Error Creating Character";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        // --------------------------------------------------------------------------------------------------- //

        // Studios
        private int StudionRemoveAs = -1;
        private void RemoveStudioAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            StudionRemoveAs = userDict[RemoveStudioAs.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Remove Studio As -> " + StudionRemoveAs);
            }
        }

        private void RemoveStudioBtn_Click(object sender, EventArgs e)
        {
            if (selectedStudioID == -1 || StudionRemoveAs == -1)
            {
                RemoveStudioStatus.Text = "Please select a studio to remove and a user to remove as";
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"EXEC RemoveStudio @StudioID = {selectedStudioID}, @UserID = {StudionRemoveAs}", conn);
                if (debug)
                {
                    Console.WriteLine("DEBUG: Remove Studio Query -> " + cmd.CommandText);
                }
                cmd.ExecuteNonQuery();
                RemoveStudioStatus.Text = "Studio Removed";
                StudiosList.Items.Clear();
            }
            catch (Exception ex)
            {
                RemoveStudioStatus.Text = "Error Removing Studio";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            updateStudio();
            resetStudioVars();
            requestStudioList();
            StudionRemoveAs = -1;
        }
        //filtervars
        private String studioFilterName = null;
        private DateTime studioFilterEstablishedAfter;
        private DateTime studioFilterEstablishedBefore;
        private int studioFilterOffset = 1;

        private int selectedStudioID = -1;
        private int lastSelectedStudioID = -1;

        private String studioDetailsName = null;
        private String studioDetailsDescription = null;
        private String studioDetailsAltName = null;
        private DateTime studioDetailsEstablished;
        private int studioUpdateAs = -1;

        private String studioCreateName = null;
        private String studioCreateDescription = null;
        private String studioCreateAltName = null;
        private DateTime studioCreateEstablished;
        private int studioCreateAs = -1;

        private void StudioTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StudioTab.SelectedIndex == 0)
            {
                StudiosList.Items.Clear();
                studioFilterOffset = 1;
                StudioPage.Value = 1;
                resetStudioVars();
                requestStudioList();
            } else if (StudioTab.SelectedIndex == 1)
            {
                requestSingleStudio();
            }
            else if (StudioTab.SelectedIndex == 2)
            {
                resetStudioCreate();
            }
        }

        private void resetStudioCreate()
        {
            StudioCreateName.Text = "";
            StudioCreateDescription.Text = "";
            StudioCreateAltName.Text = "";
            StudioCreateEstablishedAt.Text = "";
            StudioCreateAs.SelectedIndex = 0;
            studioCreateEstablished = DefaultDate;
            studioCreateAs = -1;
        }
        private void resetStudioDetails()
        {
            StudioDetailsID.Text = "";
            StudioDetailsName.Text = "";
            StudioDetailsDescription.Text = "";
            StudioDetailsAltName.Text = "";
            StudioDetailsEstablishedAt.Text = "";
        }

        private void requestSingleStudio()
        {
            if (selectedStudioID == -1 || selectedStudioID == lastSelectedStudioID)
            {
                return;
            }

            resetStudioDetails();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"Select * From Studio Where ID = {selectedStudioID}", conn);
                if (debug)
                {
                    Console.WriteLine("DEBUG: Single Studio Query -> " + cmd.CommandText);
                }
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StudioDetailsID.Text = reader["ID"].ToString();
                    StudioDetailsName.Text = reader["Name"].ToString();
                    StudioDetailsDescription.Text = reader["Description"].ToString();
                    StudioDetailsAltName.Text = reader["Alt_Name"].ToString();
                    StudioDetailsEstablishedAt.Text = reader["Established_At"].ToString();
                    Console.WriteLine(reader["Established_At"].ToString());
                }

                lastSelectedStudioID = selectedStudioID;

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void StudiosBtn_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = false;
            StudioTab.Visible = true;
            StaffTab.Visible = false;
            UsersTab.Visible = false;
            StudiosList.Items.Clear();
            studioFilterOffset = 1;
            resetStudioVars();
            requestStudioList();
        }

        private void resetStudioVars()
        {
            StudioFilterName.Text = null;
            StudioFilterAfter.Value = DateTime.Now;
            StudioFilterBefore.Value = DateTime.Now;
            Console.WriteLine(StudioFilterBefore.Text);
            studioFilterEstablishedAfter = DefaultDate;
            studioFilterEstablishedBefore = DefaultDate;
        }

        private void requestStudioList()
        {
            String command = $"EXEC FilterStudio @Name = {(studioFilterName == null ? "NULL" : "'" + studioFilterName + "'")}, @EstablishedAfter = {(studioFilterEstablishedAfter == DefaultDate ? "NULL" : "'" + studioFilterEstablishedAfter.ToString("yyyy-MM-dd") + "'")}, @EstablishedBefore = {(studioFilterEstablishedBefore == DefaultDate ? "NULL" : "'" + studioFilterEstablishedBefore.ToString("yyyy-MM-dd") + "'")}, @Offset = {(studioFilterOffset - 1) * 20}";
            if (debug)
            {
                Console.WriteLine("DEBUG: Studio List Query -> " + command);
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StudiosList.Items.Add(reader["ID"].ToString());
                    if (reader["Name"].ToString() != "")
                    {
                        StudiosList.Items[StudiosList.Items.Count - 1].SubItems.Add(reader["Name"].ToString());
                    }
                    else
                    {
                        StudiosList.Items[StudiosList.Items.Count - 1].SubItems.Add("N/A");
                    }

                    if (reader["Alt_Name"].ToString() != "")
                    {
                        StudiosList.Items[StudiosList.Items.Count - 1].SubItems.Add(reader["Alt_Name"].ToString());
                    }
                    else
                    {
                        StudiosList.Items[StudiosList.Items.Count - 1].SubItems.Add("N/A");
                    }

                    if (reader["Established_At"].ToString() != "")
                    {
                        StudiosList.Items[StudiosList.Items.Count - 1].SubItems.Add(((DateTime)reader["Established_At"]).ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        StudiosList.Items[StudiosList.Items.Count - 1].SubItems.Add("N/A");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void StudioFilterName_TextChanged(object sender, EventArgs e)
        {
            studioFilterName = StudioFilterName.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Filter Name -> " + studioFilterName);
            }
        }

        private void StudioFilterAfter_ValueChanged(object sender, EventArgs e)
        {
            studioFilterEstablishedAfter = StudioFilterAfter.Value;

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Filter After -> " + studioFilterEstablishedAfter.ToString("yyyy-MM-dd"));
            }
        }

        private void StudioFilterBefore_ValueChanged(object sender, EventArgs e)
        {
            studioFilterEstablishedBefore = StudioFilterBefore.Value;

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Filter Before -> " + studioFilterEstablishedBefore.ToString("yyyy-MM-dd"));
            }
        }

        private void StudioFilterClear_Click(object sender, EventArgs e)
        {
            resetStudioVars();
            StudiosList.Items.Clear();
            requestStudioList();
        }

        private void StudioPage_ValueChanged(object sender, EventArgs e)
        {
            studioFilterOffset = (int)StudioPage.Value;
            StudiosList.Items.Clear();
            requestStudioList();
        }

        private void StudioDetailsName_TextChanged(object sender, EventArgs e)
        {
            studioDetailsName = StudioDetailsName.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Details Name -> " + studioDetailsName);
            }
        }

        private void StudioDetailsDescription_TextChanged(object sender, EventArgs e)
        {
            studioDetailsDescription = StudioDetailsDescription.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Details Description -> " + studioDetailsDescription);
            }
        }

        private void StudioDetailsAltName_TextChanged(object sender, EventArgs e)
        {
            studioDetailsAltName = StudioDetailsAltName.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Details Alt Name -> " + studioDetailsAltName);
            }
        }

        private void StudioDetailsEstablishedAt_ValueChanged(object sender, EventArgs e)
        {
            studioDetailsEstablished = StudioDetailsEstablishedAt.Value;

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Details Established At -> " + studioDetailsEstablished.ToString("yyyy-MM-dd"));
            }
        }

        private void StudioDetailsUpdateAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            studioUpdateAs = userDict[StudioDetailsUpdateAs.SelectedItem.ToString()];

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Details Update As -> " + studioUpdateAs);
            }
        }

        private void StudioDetailsUpdateBtn_Click(object sender, EventArgs e)
        {
            if (studioUpdateAs == -1 || selectedStudioID == -1)
            {
                UpdateStudioStatus.Text = "Please select a studio and an user to update as.";
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"EXEC UpdateStudio @StudioID = {selectedStudioID}, @Name = {(studioDetailsName == null ? "NULL" : "'" + studioDetailsName + "'")}, @Description = {(studioDetailsDescription == null ? "NULL" : "'" + studioDetailsDescription + "'")}, @Alt_Name = {(studioDetailsAltName == null ? "NULL" : "'" + studioDetailsAltName + "'")}, @Established_at = {(studioDetailsEstablished == DefaultDate ? "NULL" : "'" + studioDetailsEstablished.ToString("yyyy-MM-dd") + "'")}, @UserID = {studioUpdateAs}", conn);
                if (debug)
                {
                    Console.WriteLine("DEBUG: Update Character Query -> " + cmd.CommandText);
                }
                cmd.ExecuteNonQuery();
                CharDetailStatus.Text = "Studio Updated Successfully!";
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                CharDetailStatus.Text = "Studio Update Failed!";
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            updateStudio();


        }
        private void StudioApplyFilterBtn_Click(object sender, EventArgs e)
        {
            StudiosList.Items.Clear();
            requestStudioList();
        }

        private void StudiosList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
            {
                return;
            }

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Selected -> " + e.Item.Text);
            }

            selectedStudioID = int.Parse(e.Item.Text);

        }
        private void StudioCreateName_TextChanged(object sender, EventArgs e)
        {
            studioCreateName = StudioCreateName.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Create Name -> " + studioCreateName);
            }
        }

        private void StudioCreateDescription_TextChanged(object sender, EventArgs e)
        {
            studioCreateDescription = StudioCreateDescription.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Create Description -> " + studioCreateDescription);
            }

        }

        private void StudioCreateAltName_TextChanged(object sender, EventArgs e)
        {
            studioCreateAltName = StudioCreateAltName.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Create Alt Name -> " + studioCreateAltName);
            }
        }

        private void StudioCreateEstablishedAt_ValueChanged(object sender, EventArgs e)
        {
            studioCreateEstablished = StudioCreateEstablishedAt.Value;

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Create Established At -> " + studioCreateEstablished.ToString("yyyy-MM-dd"));
            }
        }

        private void StudioCreateAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(StudioCreateAs.SelectedItem.ToString());
            studioCreateAs = userDict[StudioCreateAs.SelectedItem.ToString()];

            if (debug)
            {
                Console.WriteLine("DEBUG: Studio Create As -> " + studioCreateAs);
            }
        }

        private void StudioCreateBtn_Click(object sender, EventArgs e)
        {
            if (studioCreateAs == -1 || studioCreateName == null)
            {
                CreateStudioStatus.Text = "Please select a user and a name for the studio.";
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"EXEC CreateStudio @Name = '{studioCreateName}', @Description = {(studioCreateDescription == null ? "NULL" : "'" + studioCreateDescription + "'")}, @Alt_Name = {(studioCreateAltName == null ? "NULL" : "'" + studioCreateAltName + "'")}, @Established_at = {(studioCreateEstablished == DefaultDate ? "NULL" : "'" + studioCreateEstablished.ToString("yyyy-MM-dd") + "'")}, @UserID = {studioCreateAs}", conn);
                if (debug)
                {
                    Console.WriteLine("DEBUG: Create Studio Query -> " + cmd.CommandText);
                }
                cmd.ExecuteNonQuery();
                CreateStudioStatus.Text = "Studio Created Successfully!";
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                CreateStudioStatus.Text = "Studio Creation Failed!";
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            updateStudio();
        }
        // --------------------------------------------------------------------------------------------------- //

        // Staff
        private int userRemoveEntityAs = -1;
        private void StaffRemoveAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            userRemoveEntityAs = userDict[StaffRemoveAs.SelectedItem.ToString()];

            if (debug)
            {
                Console.WriteLine("DEBUG: Staff Remove As -> " + userRemoveEntityAs);
            }
        }

        private void RemoveStaff_Click(object sender, EventArgs e)
        {
            if (userRemoveEntityAs == -1 || selectedStaffID == -1)
            {
                RemoveStaffStatus.Text = "Please select a staff and an user to remove as.";
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"EXEC RemoveStaff @StaffID = {selectedStaffID}, @UserID = {userRemoveEntityAs}", conn);
                if (debug)
                {
                    Console.WriteLine("DEBUG: Remove Staff Query -> " + cmd.CommandText);
                }
                cmd.ExecuteNonQuery();
                RemoveStaffStatus.Text = "Staff Removed Successfully!";
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                RemoveStaffStatus.Text = "Staff Removal Failed!";
            }
            
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            updateVA();
            StaffList.Items.Clear();
            requestStaffList();
            userRemoveEntityAs = -1;
        }

        private int staffOffset = 1;
        private String staffFilterName = null;
        private String staffFilterRole = null;

        // staff details
        private int selectedStaffID = -1;
        private int lastSelectedStaffID = -1;
        private String staffDetailsName = null;
        private String staffDetailsType = null;
        private DateTime staffDetailsDOB;
        private int staffDetailsUpdateAs = -1;

        // staff create
        private String staffCreateName = null;
        private String staffCreateType = null;
        private DateTime staffCreateDOB;
        private int staffCreateAs = -1;

        private void StaffBtn_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = false;
            StudioTab.Visible = false;
            StaffTab.Visible = true;
            UsersTab.Visible = false;
            StaffListPage.Value = 1;
            staffOffset = 1;
            resetStaffVar();
            StaffList.Items.Clear();
            requestStaffList();
        }

        private void resetStaffVar()
        {
            staffFilterName = null;
            staffFilterRole = null;

            StaffTabFilterName.Text = "";
            StaffFilterType.SelectedIndex = 0;
        }

        private void StaffTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StaffTab.SelectedIndex == 0)
            {
                StaffListPage.Value = 1;
                staffOffset = 1;
                resetStaffVar();
                StaffList.Items.Clear();
                requestStaffList();
            }
            else if (StaffTab.SelectedIndex == 1)
            {
                requestSingleStaff();
            }
            else if (StaffTab.SelectedIndex == 2)
            {
                resetStaffCreate();
            }

        }

        private void resetStaffDetails()
        {
            StaffDetailsID.Text = "";
            StaffDetailsName.Text = "";
            StaffDetailsType.SelectedIndex = 0;
            StaffDetailsBirthday.Text = "";
            StaffUpdateStatus.Text = "";
        }

        private void requestSingleStaff()
        {
            if (selectedStaffID == -1 || selectedStaffID == lastSelectedStaffID)
            {
                return;
            }

            resetStaffDetails();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"Select * From Staff Where ID = {selectedStaffID}", conn);

                if (debug)
                {
                    Console.WriteLine("DEBUG: Single Staff Query -> " + cmd.CommandText);
                }

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    StaffDetailsID.Text = reader["ID"].ToString();
                    StaffDetailsName.Text = reader["Name"].ToString();
                    StaffDetailsType.Text = reader["Type"].ToString();
                    StaffDetailsBirthday.Text = reader["Birthday"].ToString();
                }

                lastSelectedStaffID = selectedStaffID;
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void requestStaffList()
        {
            String command = $"EXEC FilterStaff @Offset = {(staffOffset - 1) * 20}, @Name = {(staffFilterName == null ? "NULL" : "'" + staffFilterName + "'")}, @Type = {(staffFilterRole == null ? "NULL" : "'" + staffFilterRole + "'")}";
            try
            {
                if (debug)
                {
                    Console.WriteLine("DEBUG: Staff List Query -> " + command);
                }
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StaffList.Items.Add(reader["ID"].ToString());
                    StaffList.Items[StaffList.Items.Count - 1].SubItems.Add(reader["Name"].ToString());
                    StaffList.Items[StaffList.Items.Count - 1].SubItems.Add(reader["Type"].ToString());
                    if (reader["Birthday"].ToString() == "")
                    {
                        StaffList.Items[StaffList.Items.Count - 1].SubItems.Add("N/A");
                    }
                    else
                    {
                        StaffList.Items[StaffList.Items.Count - 1].SubItems.Add(((DateTime)reader["Birthday"]).ToString("yyyy-MM-dd"));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void StaffTabFilterName_TextChanged(object sender, EventArgs e)
        {
            staffFilterName = StaffTabFilterName.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Staff Filter Name -> " + staffFilterName);
            }
        }

        private void StaffFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            staffFilterRole = StaffFilterType.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Staff Filter Role -> " + staffFilterRole);
            }
        }

        private void StaffApplyFilterBtn_Click(object sender, EventArgs e)
        {
            StaffList.Items.Clear();
            requestStaffList();
        }

        private void StaffClearFilter_Click(object sender, EventArgs e)
        {
            resetStaffVar();
            StaffList.Items.Clear();
            requestStaffList();
        }

        private void StaffListPage_ValueChanged(object sender, EventArgs e)
        {
            staffOffset = (int)StaffListPage.Value;
            StaffList.Items.Clear();
            requestStaffList();
        }

        private void StaffList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
            {
                return;
            }

            if (debug)
            {
                Console.WriteLine("DEBUG: Staff List Selected -> " + e.Item.Text);
            }
            selectedStaffID = int.Parse(e.Item.Text);
        }

        private void StaffDetailsBirthday_ValueChanged(object sender, EventArgs e)
        {
            staffDetailsDOB = StaffDetailsBirthday.Value;

            if (debug)
            {
                Console.WriteLine("DEBUG: Staff Details DOB -> " + staffDetailsDOB.ToString("yyyy-MM-dd"));
            }
        }

        private void StaffDetailsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            staffDetailsType = StaffDetailsType.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: Staff Details Type -> " + staffDetailsType);
            }
        }

        private void StaffDetailsName_TextChanged(object sender, EventArgs e)
        {
            staffDetailsName = StaffDetailsName.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: Staff Details Name -> " + staffDetailsName);
            }
        }

        private void StaffDetailsUpdateAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            staffDetailsUpdateAs = userDict[StaffDetailsUpdateAs.Text];

            if (debug)
            {
                Console.WriteLine("DEBUG: Staff Details Update As -> " + staffDetailsUpdateAs);
            }
        }

        private void StaffDetailsUpdateBtn_Click(object sender, EventArgs e)
        {
            if (staffDetailsUpdateAs == -1 || selectedStaffID == -1)
            {
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"EXEC UpdateStaff @StaffID = {selectedStaffID}, @Name = '{staffDetailsName}', @Type = '{staffDetailsType}', @Birthday = '{staffDetailsDOB.ToString("yyyy-MM-dd")}', @UserID = {staffDetailsUpdateAs}", conn);
                if (debug)
                {
                    Console.WriteLine("DEBUG: Staff Update Query -> " + cmd.CommandText);
                }

                cmd.ExecuteNonQuery();
                StaffUpdateStatus.Text = "Updated Successfully";
            }
            catch (Exception ex)
            {
                StaffUpdateStatus.Text = "Update Failed";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            updateVA();
        }

        private void resetStaffCreate()
        {
            StaffCreateName.Text = "";
            StaffCreateType.SelectedIndex = 0;
            StaffCreateBirthday.Value = DateTime.Now;
            StaffCreateAs.SelectedIndex = 0;
            StaffCreateStatus.Text = "";
        }

        private void StaffCreateName_TextChanged(object sender, EventArgs e)
        {
            staffCreateName = StaffCreateName.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Staff Create Name -> " + staffCreateName);
            }
        }

        private void StaffCreateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            staffCreateType = StaffCreateType.Text;
            if (debug)
            {
                Console.WriteLine("DEBUG: Staff Create Type -> " + staffCreateType);
            }
        }

        private void StaffCreateBirthday_ValueChanged(object sender, EventArgs e)
        {
            staffCreateDOB = StaffCreateBirthday.Value;
            if (debug)
            {
                Console.WriteLine("DEBUG: Staff Create DOB -> " + staffCreateDOB.ToString("yyyy-MM-dd"));
            }
        }

        private void StaffCreateAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            staffCreateAs = userDict[StaffCreateAs.Text];
            if (debug)
            {
                Console.WriteLine("DEBUG: Staff Create As -> " + staffCreateAs);
            }
        }

        private void StaffCreateBtn_Click(object sender, EventArgs e)
        {
            if (staffCreateAs == -1 || staffCreateName == "" || staffCreateType == "" || staffCreateDOB == null)
            {
                StaffCreateStatus.Text = "Please fill all fields";
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"EXEC CreateStaff @Name = '{staffCreateName}', @Type = '{staffCreateType}', @Birthday = '{staffCreateDOB.ToString("yyyy-MM-dd")}', @UserID = {staffCreateAs}", conn);
                if (debug)
                {
                    Console.WriteLine("DEBUG: Staff Create Query -> " + cmd.CommandText);
                }

                cmd.ExecuteNonQuery();
                StaffCreateStatus.Text = "Created Successfully";
                resetStaffCreate();
            }
            catch (Exception ex)
            {
                StaffCreateStatus.Text = "Create Failed";
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            updateVA();
        }

        // --------------------------------------------------------------------------------------------------- //
        // Users

        private int selectedUser = -1;
        private int lastSelectedUser = -1;

        private String FilterUserUsername = "";
        private String FilterUserSex = "";
        private DateTime FilterBirthday;
        private DateTime FilterCreatedAfter;
        private DateTime FilterCreatedBefore;
        private int FilterUserOffset = 1;

        // details
        private String userDetailsName = "";
        private String userDetailsSex = "";
        private DateTime userDetailsDOB;
        private String userDetailsLocation = "";

        private int userDetailsSelectedAnimeID = -1;
        private int userDetailsAddNewAnimeID = -1;
        private float userDetailsExistingRating = -1;
        private float userDetailsNewRating = -1;

        private int selectedFriend = -1;
        private int selectedNewFriend = -1;

        private String userCreateUsername = "";
        private String userCreateSex = "";
        private DateTime userCreateDOB;
        private String userCreateLocation = "";
        private String userIsadmin = "";
        // create

        private void UserBtn_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = false;
            StudioTab.Visible = false;
            StaffTab.Visible = false;
            UsersTab.Visible = true;
            FilterUserOffset = 1;
            UserList.Items.Clear();
            resetUserVar();
            requestUserList();
        }

        private void UsersTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UsersTab.SelectedIndex == 0)
            {
                UserList.Items.Clear();
                requestUserList();
            }
            else if (UsersTab.SelectedIndex == 1)
            {
                requestUserDetails();
            }
            else if (UsersTab.SelectedIndex == 2)
            {
                resetUserCreate();
            }
        }

        private void resetUserVar()
        {
            UserNameFilter.Text = "";
            UserMaleFilter.Checked = false;
            UserFemaleFilter.Checked = false;
            UserBirthdayFilter.Value = DateTime.Now;
            UserCreatedAfterFilter.Value = DateTime.Now;
            UserCreatedBeforeFilter.Value = DateTime.Now;

            FilterUserSex = "";
            FilterUserUsername = "";
            FilterBirthday = DefaultDate;
            FilterCreatedAfter = DefaultDate;
            FilterCreatedBefore = DefaultDate;
        }

        private void requestUserList()
        {
            String commnad = $@"EXEC FilterUser @Name = {(FilterUserUsername == "" ? "NULL" : $"'{FilterUserUsername}'")}, @Sex = {(FilterUserSex == "" ? "NULL" : $"'{FilterUserSex}'")}, @Birthday = {(FilterBirthday == DefaultDate ? "NULL" : $"'{FilterBirthday.ToString("yyyy-MM-dd")}'")}, @CreatedAfter = {(FilterCreatedAfter == DefaultDate ? "NULL" : $"'{FilterCreatedAfter.ToString("yyyy-MM-dd")}'")}, @CreatedBefore = {(FilterCreatedBefore == DefaultDate ? "NULL" : $"'{FilterCreatedBefore.ToString("yyyy-MM-dd")}'")}, @Offset = {(FilterUserOffset-1)*20}";

            if (debug)
            {
                Console.WriteLine("DEBUG: User Filter Query -> " + commnad);
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(commnad, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserList.Items.Add(reader["ID"].ToString());
                    UserList.Items[UserList.Items.Count - 1].SubItems.Add(reader["Name"].ToString());
                    UserList.Items[UserList.Items.Count - 1].SubItems.Add(reader["Sex"].ToString());
                    UserList.Items[UserList.Items.Count - 1].SubItems.Add(((DateTime)reader["Created_Date"]).ToString("yyyy-MM-dd"));
                    UserList.Items[UserList.Items.Count - 1].SubItems.Add(((DateTime)reader["Birthday"]).ToString("MM-dd"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void resetUserCreate()
        {
            UserCreateName.Text = "";
            UserCreateMaleCheckbox.Checked = false;
            UserCreateFemaleCheckbox.Checked = false;
            UserCreateBirthday.Value = DateTime.Now;
            UserCreateLocation.Text = "";
            UserCreateIsAdmin.Checked = false;
            CreationStatus.Text = "";
        }

        private void UserList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
            {
                return;
            }
            if (debug)
            {
                Console.WriteLine("DEBUG: Selected User ID -> " + e.Item.Text);
            }
            selectedUser = int.Parse(e.Item.Text);
        }

        private void UserNameFilter_TextChanged(object sender, EventArgs e)
        {
            FilterUserUsername = UserNameFilter.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: User Filter Name -> " + FilterUserUsername);
            }
        }

        private void UserMaleFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (UserFemaleFilter.Checked && UserMaleFilter.Checked)
            {
                UserFemaleFilter.Checked = false;
            }
            FilterUserSex = UserMaleFilter.Checked ? "M" : "";

            if (debug)
            {
                Console.WriteLine("DEBUG: User Sex -> " + FilterUserSex);
            }
        }

        private void UserFemaleFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (UserFemaleFilter.Checked && UserMaleFilter.Checked)
            {
                UserMaleFilter.Checked = false;
            }
            FilterUserSex = UserFemaleFilter.Checked ? "F" : "";

            if (debug)
            {
                Console.WriteLine("DEBUG: User Sex -> " + FilterUserSex);
            }
        }

        private void UserBirthdayFilter_ValueChanged(object sender, EventArgs e)
        {
            FilterBirthday = UserBirthdayFilter.Value;

            if (debug)
            {
                Console.WriteLine("DEBUG: User Birthday -> " + FilterBirthday.ToString("yyyy-MM-dd"));
            }
        }

        private void UserCreatedAfterFilter_ValueChanged(object sender, EventArgs e)
        {
            FilterCreatedAfter = UserCreatedAfterFilter.Value;

            if (debug)
            {
                Console.WriteLine("DEBUG: User Created After -> " + FilterCreatedAfter.ToString("yyyy-MM-dd"));
            }
        }

        private void UserCreatedBeforeFilter_ValueChanged(object sender, EventArgs e)
        {
            FilterCreatedBefore = UserCreatedBeforeFilter.Value;

            if (debug)
            {
                Console.WriteLine("DEBUG: User Created Before -> " + FilterCreatedBefore.ToString("yyyy-MM-dd"));
            }
        }

        private void UsersApplyFilterBtn_Click(object sender, EventArgs e)
        {
            UserList.Items.Clear();
            requestUserList();
        }

        private void UserClearFilter_Click(object sender, EventArgs e)
        {
            resetUserVar();
            UserList.Items.Clear();
            requestUserList();
        }

        private void UserListPage_ValueChanged(object sender, EventArgs e)
        {
            FilterUserOffset = (int)UserListPage.Value;
            UserList.Items.Clear();
            requestUserList();
        }

        private void requestUserDetails()
        {
            if (selectedUser == -1 || selectedUser == lastSelectedUser)
            {
                return;
            }

            resetUserDetails();
            
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Users WHERE ID = {selectedUser}", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (debug)
                {               
                    Console.WriteLine("DEBUG: User Details Query -> " + cmd.CommandText);
                }

                if (reader.Read())
                {
                    UserDetailsID.Text = reader["ID"].ToString();
                    UserDetailsName.Text = reader["Name"].ToString();
                    if (reader["Sex"].ToString() == "M")
                    {
                        UserDetailsMale.Checked = true;
                    }
                    else
                    {
                        UserDetailsFemale.Checked = true;
                    }

                    UserDetailsBirthday.Value = (DateTime)reader["Birthday"];
                    UserDetailsCreatedDate.Text = ((DateTime)reader["Created_Date"]).ToString("yyyy-MM-dd");
                    UserDetailsLocation.Text = reader["Location"].ToString();
                    Console.WriteLine(reader["is_admin"].ToString());
                    if (reader["is_admin"].ToString() == "1")
                    {
                        UserDetailsIsAdmin.Checked = true;
                    }
                    else
                    {
                        UserDetailsIsAdmin.Checked = false;
                    }
                                lastSelectedUser = selectedUser;

                }

                reader.Close();
                
                getWatchedAnime();

               
                getFriends();
                lastSelectedUser = selectedUser;
            }

            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }

        private void getWatchedAnime()
        {
            SqlCommand cmd = new SqlCommand($"EXEC GetWatchedAnimes @UserID = {selectedUser}", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                UserDetailsAnimeList.Items.Add(new ScoreItem { ID = (int)reader["AnimeID"], Score = reader["Given_score"].ToString(), DisplayName = reader["AnimeName"].ToString() });
            }
            reader.Close();
        }
        private void resetUserDetails()
        {
            UserDetailsID.Text = "";
            UserDetailsName.Text = "";
            UserDetailsMale.Checked = false;
            UserDetailsFemale.Checked = false;
            UserDetailsBirthday.Value = DateTime.Now;
            UserDetailsCreatedDate.Text = "";
            UserDetailsLocation.Text = "";
            UserDetailsAnimeList.Items.Clear();
            UserDetailsAnimeRating.Value = 0;
            userDetailsExistingRating = 0;
            userDetailsSelectedAnimeID = -1;
            userDetailsSex = "";
            RatingStatus.Text = "";
            UserUpdateStatus.Text = "";
        }

        private void UserDetailsAnimeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserDetailsAnimeList.SelectedItems.Count == 0)
            {
                return;
            }

            ScoreItem sc = UserDetailsAnimeList.SelectedItem as ScoreItem;

            UserDetailsAnimeRating.Value = decimal.Parse(sc.Score);
            userDetailsSelectedAnimeID = sc.ID;
            UserDetailsSelAnime.Text = sc.DisplayName;
        }

        private void UserDetailsAnimeRating_ValueChanged(object sender, EventArgs e)
        {
            userDetailsExistingRating = (float)UserDetailsAnimeRating.Value;


            if (debug)
            {
                Console.WriteLine("DEBUG: User Details Anime Rating -> " + userDetailsExistingRating);
            }
        }

        private void UserDetailsMale_CheckedChanged(object sender, EventArgs e)
        {
            if (UserDetailsFemale.Checked && UserDetailsMale.Checked)
            {
                UserDetailsFemale.Checked = false;
            }

            userDetailsSex = UserDetailsMale.Checked ? "M" : "";

            if (debug)
            {
                Console.WriteLine("DEBUG: User Details Sex -> " + userDetailsSex);
            }
        }

        private void UserDetailsFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (UserDetailsFemale.Checked && UserDetailsMale.Checked)
            {
                UserDetailsMale.Checked = false;
            }

            userDetailsSex = UserDetailsFemale.Checked ? "F" : "";

            if (debug)
            {
                Console.WriteLine("DEBUG: User Details Sex -> " + userDetailsSex);
            }
        }

        private void UserDetailsName_TextChanged(object sender, EventArgs e)
        {
            userDetailsName = UserDetailsName.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: User Details Name -> " + userDetailsName);
            }
        }

        private void UserDetailsLocation_TextChanged(object sender, EventArgs e)
        {
            userDetailsLocation = UserDetailsLocation.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: User Details Location -> " + userDetailsLocation);
            }
        }


        private void UserDetailsBirthday_ValueChanged(object sender, EventArgs e)
        {
            userDetailsDOB = UserDetailsBirthday.Value;

            if (debug)
            {
                Console.WriteLine("DEBUG: User Details Birthday -> " + userDetailsDOB.ToString("MM-dd"));
            }
        }

        private void UserDetailsUpdateInfoBtn_Click(object sender, EventArgs e)
        {
            bool isChanged = false;
            if (selectedUser == -1)
            {
                return;
            }

            try
            {
                conn.Open();
                String cmd = $"EXEC UpdateUser @UserID = {selectedUser}, @Name = '{userDetailsName}', @Sex = '{userDetailsSex}', @Birthday = '{userDetailsDOB.ToString("yyyy-MM-dd")}', @Location = '{userDetailsLocation}'";
                SqlCommand sqlCmd = new SqlCommand(cmd, conn);
                sqlCmd.ExecuteNonQuery();
                UserUpdateStatus.Text = "User details updated successfully";
                isChanged = true;
            }

            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            if (isChanged)
            {
                updateUsers();
            }
        }

        private void UserDetailsUpdateScoreBtn_Click(object sender, EventArgs e)
        {
            if (userDetailsSelectedAnimeID == -1)
            {
                RatingStatus.Text = "Please select an anime to update";
                return;
            }

            try
            {
                conn.Open();
                String cmd = $"UPDATE Has_Watched SET Given_score = {userDetailsExistingRating} WHERE FK_UserID = {selectedUser} AND FK_AnimeID = {userDetailsSelectedAnimeID}";
                SqlCommand sqlCmd = new SqlCommand(cmd, conn);
                sqlCmd.ExecuteNonQuery();
                RatingStatus.Text = "Anime rating updated successfully";
                UserDetailsAnimeList.Items.Clear();
                getWatchedAnime();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                RatingStatus.Text = "Error updating anime rating";
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void UserSelectNewAnimeRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            userDetailsAddNewAnimeID = animeDict[UserSelectNewAnimeRating.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Anime Studio ID -> " + userDetailsAddNewAnimeID);
            }

        }

 

        private void UserDetailsAddScoreBtn_Click(object sender, EventArgs e)
        {
            if (userDetailsAddNewAnimeID == -1)
            {
                RatingStatus.Text = "Please select an anime to add";
                return;
            }

            try
            {
                conn.Open();
                String cmd = $"INSERT INTO Has_Watched (FK_UserID, FK_AnimeID, Given_score) VALUES ({selectedUser}, {userDetailsAddNewAnimeID}, {userDetailsNewRating})";
                SqlCommand sqlCmd = new SqlCommand(cmd, conn);
                sqlCmd.ExecuteNonQuery();
                RatingStatus.Text = "Anime rating added successfully";
                UserDetailsAnimeList.Items.Clear();
                getWatchedAnime();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                RatingStatus.Text = "Error adding anime rating";
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void RemoveWatchedBtn_Click(object sender, EventArgs e)
        {
            if (userDetailsSelectedAnimeID == -1)
            {
                RatingStatus.Text = "Please select an anime to remove";
                return;
            }

            try
            {
                conn.Open();
                String cmd = $"DELETE FROM Has_Watched WHERE FK_UserID = {selectedUser} AND FK_AnimeID = {userDetailsSelectedAnimeID}";
                SqlCommand sqlCmd = new SqlCommand(cmd, conn);
                sqlCmd.ExecuteNonQuery();
                RatingStatus.Text = "Anime rating removed successfully";
                UserDetailsAnimeList.Items.Clear();
                getWatchedAnime();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                RatingStatus.Text = "Error removing anime rating";
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void addRating_ValueChanged(object sender, EventArgs e)
        {
            userDetailsNewRating = (float)addRating.Value;

            if (debug)
            {
                Console.WriteLine("DEBUG: User Details New Rating -> " + userDetailsNewRating);
            }
        }

        private void UserCreateName_TextChanged(object sender, EventArgs e)
        {
            userCreateUsername = UserCreateName.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: User Create Username -> " + userCreateUsername);
            }
        }

        private void UserCreateLocation_TextChanged(object sender, EventArgs e)
        {
            userCreateLocation = UserCreateLocation.Text;

            if (debug)
            {
                Console.WriteLine("DEBUG: User Create Location -> " + userCreateLocation);
            }
        }

        private void UserCreateMaleCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (UserCreateMaleCheckbox.Checked && UserCreateFemaleCheckbox.Checked)
            {
                UserCreateFemaleCheckbox.Checked = false;
            }

            userCreateSex = UserCreateMaleCheckbox.Checked ? "M" : "";

            if (debug)
            {
                Console.WriteLine("DEBUG: User Sex ->" + userCreateSex);
            }

        }

        private void UserCreateFemaleCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (UserCreateMaleCheckbox.Checked && UserCreateFemaleCheckbox.Checked)
            {
                UserCreateMaleCheckbox.Checked = false;
            }

            userCreateSex = UserCreateFemaleCheckbox.Checked ? "F" : "";

            if (debug)
            {
                Console.WriteLine("DEBUG: User Sex -> " + userCreateSex);
            }
                
        }

        private void UserCreateBirthday_ValueChanged(object sender, EventArgs e)
        {
            userCreateDOB = UserCreateBirthday.Value;

            if (debug)
            {
                Console.WriteLine("DEBUG: User DOB -> " + userCreateDOB);
            }
        }

        private void UserCreateIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (UserCreateIsAdmin.Checked)
            {
                userIsadmin = "1";
            } else
            {
                userIsadmin = "0";
            }
            
            if (debug)
            {
                Console.WriteLine("DEBUG: User Is Admin -> " + userIsadmin);
            }
        }

        private void CreateUserBtn_Click(object sender, EventArgs e)
        {
            if (userCreateUsername == "" || userCreateLocation == "" || userCreateSex == "" || userCreateDOB == null)
            {
                CreationStatus.Text = "Please fill out all fields";
                return;
            }

            String command = $"EXEC CreateUser @Name = '{userCreateUsername}', @Location = '{userCreateLocation}', @Sex  = '{userCreateSex}', @Birthday = '{userCreateDOB.ToString("yyyy-MM-dd")}', @IsAdmin = '{userIsadmin}'";

            try
            {
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand(command, conn);
                sqlCmd.ExecuteNonQuery();
                CreationStatus.Text = "User created successfully";
                resetUserCreate();
            }

            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                CreationStatus.Text = "Error creating user";
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            updateUsers();
        }
         
        private int UserRemoveAsID = -1;
        private void UserRemoveBtn_Click(object sender, EventArgs e)
        {
            if (UserRemoveAsID == -1 || selectedUser == -1)
            {
                UserRemoveStatus.Text = "Please select a user to remove and a user to remove as";
                return;
            }

            String command = $"EXEC RemoveUser @UserID = {selectedUser}, @AdminID = {UserRemoveAsID}";
            if (debug)
            {
                Console.WriteLine("DEBUG: User Remove Command -> " + command);
            }
            try
            {
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand(command, conn);
                sqlCmd.ExecuteNonQuery();
                UserRemoveStatus.Text = "User removed successfully";
            }

            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                UserRemoveStatus.Text = "Error removing user";
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            updateUsers();
            UserList.Items.Clear();
            requestUserList();
            selectedUser = -1;
        }

        private void UserRemoveAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserRemoveAsID = userDict[UserRemoveAs.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: User Remove As -> " + UserRemoveAsID);
            }
        }
        private void getFriends()
        {
            SqlCommand sqlCommand = new SqlCommand($"EXEC GetFriends @UserID = {selectedUser}", conn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            UserFriendsList.Items.Clear();

            while (reader.Read())
            {
                UserFriendsList.Items.Add(new User { ID = (int)reader["FriendID"], Name = reader["FriendName"].ToString() });
            }   
            reader.Close();
        }

        private void UserFriendsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserFriendsList.SelectedIndex == -1)
            {
                return;
            }

            selectedFriend = userDict[UserFriendsList.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Selected Friend -> " + selectedFriend);
            }
        }

        private void UserRemoveFriend_Click(object sender, EventArgs e)
        {
            if (selectedFriend == -1 || selectedUser == -1)
            {
                UserFriendStatus.Text = "Please select a user and a friend to remove";
                return;
            }

            String command = $"DELETE FROM Is_Friend WHERE UserID1 = {selectedUser} AND UserID2 = {selectedFriend}";
            if (debug)
            {
                Console.WriteLine("DEBUG: User Remove Friend Command -> " + command);
            }

            try
            {
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand(command, conn);
                sqlCmd.ExecuteNonQuery();
                UserFriendStatus.Text = "Friend removed successfully";
                getFriends();

            }

            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                UserFriendStatus.Text = "Error removing friend";
            }       

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }

        private void UserAddFriend_Click(object sender, EventArgs e)
        {
            if (selectedNewFriend == -1 || selectedUser == -1 && selectedNewFriend != selectedUser) 
            {
                UserFriendStatus.Text = "Please select a user and a friend to add";
                return;
            }

            String command = $"INSERT INTO Is_friend (UserID1, UserID2) VALUES ({selectedUser}, {selectedNewFriend})";
            String command2 = $"INSERT INTO Is_friend (UserID1, UserID2) VALUES ({selectedNewFriend}, {selectedUser})";
            if (debug)
            {
                Console.WriteLine("DEBUG: User Add Friend Command -> " + command);
            }

            try
            {
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand(command, conn);
                sqlCmd.ExecuteNonQuery();
                
                sqlCmd = new SqlCommand(command2, conn);
                sqlCmd.ExecuteNonQuery();

                UserFriendStatus.Text = "Friend added successfully";
                getFriends();

            }

            catch (Exception ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                UserFriendStatus.Text = "Error adding friend";
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }

        private void UserSelectNewFriend_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserSelectNewFriend.SelectedIndex == -1)
            {
                return;
            }

            selectedNewFriend = userDict[UserSelectNewFriend.SelectedItem.ToString()];
            if (debug)
            {
                Console.WriteLine("DEBUG: Selected New Friend -> " + selectedNewFriend);
            }
        }
    }
}

public class ScoreItem
{
    public int ID { get; set; }
    public String Score { get; set; }
    public string DisplayName { get; set; }

    public override string ToString()
    {
        return DisplayName;
    }
}

public class User
{
    public int ID { get; set; }
    public String Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}