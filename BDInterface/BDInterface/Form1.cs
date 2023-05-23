namespace BDInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = true;
            CharactersTab.Visible = false;
            StudioTab.Visible = false;
            StaffTab.Visible = false;
            UsersTab.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = true;
            StudioTab.Visible = false;
            StaffTab.Visible = false;
            UsersTab.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = false;
            StudioTab.Visible = true;
            StaffTab.Visible = false;
            UsersTab.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = false;
            StudioTab.Visible = false;
            StaffTab.Visible = true;
            UsersTab.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = false;
            StudioTab.Visible = false;
            StaffTab.Visible = false;
            UsersTab.Visible = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}