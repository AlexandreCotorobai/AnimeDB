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
        private DateTime DefaultDate = new DateTime(1753, 1, 1);
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
        private int lastSearchedAnimeIDComments;

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
            FilterAnimeOffset = 0;
        }

        private void requestAnimeList()
        {
            String command = $"EXEC FilterAnime @Name = {(FilterAnimeSearch == null ? "NULL" : "'" + FilterAnimeSearch + "'")}, @MinScore = {(FilterAnimeScore == 0.0 ? "NULL" : FilterAnimeScore.ToString())}, @MinDate = {(FilterAnimeStartDate == DefaultDate ? "NULL" : "'" + FilterAnimeStartDate.ToString("yyyy-MM-dd") + "'")}, @MaxDate = {(FilterAnimeEndDate == DefaultDate ? "NULL" : "'" + FilterAnimeEndDate.ToString("yyyy-MM-dd") + "'")}, @Offset = {FilterAnimeOffset * 20}";
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
            if ( SelectedAnimeID == -1 || animeRelatedAnime == 0 || animeUpdateAs == 0)
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
            if ( SelectedAnimeID == -1 || animeUpdateAs == 0 || animeAddRelation == "")
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
            if (SelectedAnimeID == -1)
            {
                return;
            }
            resetComments();

            try
            {
                conn.Open();
                updateCommentList();
            }
            catch(Exception ex)
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
