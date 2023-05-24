<<<<<<< Updated upstream:BDProject/BDProject/Form1.cs
<<<<<<< Updated upstream:BDProject/BDProject/Form1.cs
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
=======
=======
>>>>>>> Stashed changes:BDInterface/BDInterface/Form1.cs

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
<<<<<<< Updated upstream:BDProject/BDProject/Form1.cs
>>>>>>> Stashed changes:BDInterface/BDInterface/Form1.cs
=======
>>>>>>> Stashed changes:BDInterface/BDInterface/Form1.cs
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

<<<<<<< Updated upstream:BDProject/BDProject/Form1.cs
<<<<<<< Updated upstream:BDProject/BDProject/Form1.cs
namespace BDProject
=======
=======
>>>>>>> Stashed changes:BDInterface/BDInterface/Form1.cs
namespace BDInterface
>>>>>>> Stashed changes:BDInterface/BDInterface/Form1.cs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CharactersBtn_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = true;
            StudioTab.Visible = false;
            StaffTab.Visible = false;
            UsersTab.Visible = false;
        }

        private void AnimeBtn_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = true;
            CharactersTab.Visible = false;
            StudioTab.Visible = false;
            StaffTab.Visible = false;
            UsersTab.Visible = false;
        }

        private void StudiosBtn_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = false;
            StudioTab.Visible = true;
            StaffTab.Visible = false;
            UsersTab.Visible = false;
        }

        private void StaffBtn_Click(object sender, EventArgs e)
        {
            AnimeTab.Visible = false;
            CharactersTab.Visible = false;
            StudioTab.Visible = false;
            StaffTab.Visible = true;
            UsersTab.Visible = false;
        }

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
