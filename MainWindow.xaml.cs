using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace Gladiator
{
    public partial class MainWindow : Window
    {
        dbGladiatorDataContext db = new dbGladiatorDataContext();
        bool lootVar = false; // loot var is used to determine if buttons activate combat or select loot


        public MainWindow()
        {
            InitializeComponent();

            btn_Begin.IsEnabled = true;

            btn_Begin.IsEnabled = true; // sets reset button available
            btn_Center.IsEnabled = false;// sets lower buttons to unavailable
            btn_Left.IsEnabled = false;// sets lower buttons to unavailable
            btn_Right.IsEnabled = false;// sets lower buttons to unavailable

        }



        class StatData<hp, dmg, def, engy> //generic class for stat storing
        {
            public hp health { get; set; }
            public dmg damage { get; set; }
            public def defense { get; set; }
            public engy energy { get; set; }

        }

        private void btn_Center_Click(object sender, RoutedEventArgs e)
        {
            StatData<int, int, int, int> playerStats = new StatData<int, int, int, int>();
            playerStats.health = Convert.ToInt32(lbl_Health.Content);// gets player data from form
            playerStats.damage = Convert.ToInt32(lbl_Damage.Content);// gets player data from form
            playerStats.defense = Convert.ToInt32(lbl_Defense.Content);// gets player data from form
            playerStats.energy = Convert.ToInt32(lbl_Energy.Content);// gets player data from form
            int opHealth = Convert.ToInt32(lbl_HealthOp.Content);
            int level = Convert.ToInt32(lbl_level.Content);
            if (lootVar == false && playerStats.health > 0)
            {
                string playerDirection = "center"; // sets string to center for combat mechanic
                combat(playerDirection); // activates combat mechanic on clicck

            }
            else if (lootVar)
            {

                string selectedItem = lbl_ItemCenter.Content.ToString();
                var ItemStats = from x in db.itemLists where x.itemName.Equals(selectedItem) select x;
                foreach (var item in ItemStats)// item is check for its stat affect to determine where to equip it.
                {
                    if (item.statAffect.Equals("health"))
                    {
                        lbl_HealthEquip.Content = item.itemName.ToString();
                        playerStats.health += Convert.ToInt32(item.statIncrease);
                        lbl_Health.Content = playerStats.health.ToString();
                    }
                    else if (item.statAffect.Equals("damage"))
                    {
                        int dmgIncrease = Convert.ToInt32(item.statIncrease);
                        lbl_DamageEquip.Content = item.itemName.ToString();
                        playerStats.damage += dmgIncrease;
                        lbl_Damage.Content = playerStats.damage.ToString();
                    }
                    else if (item.statAffect.Equals("defense"))
                    {
                        lbl_DefenseEquip.Content = item.itemName.ToString();
                        int defIncrease = Convert.ToInt32(lbl_Defense.Content);
                        playerStats.defense += Convert.ToInt32(item.statIncrease);
                        lbl_Defense.Content = playerStats.defense.ToString();
                    }
                    else if(item.statAffect.Equals("energy"))
                    {
                        int engyIncrease = Convert.ToInt32(item.statIncrease);
                        lbl_EnergyEquip.Content = item.itemName.ToString();
                        playerStats.energy += engyIncrease;
                        lbl_Energy.Content = playerStats.energy.ToString();
                    }
                }
                buttonStatus();
            }
            
        }
        
        

        private void mouseover_center(object sender, RoutedEventArgs e)
        {
            
            Thread thread = new Thread(threadControl_Center); // for mouse over control
            thread.Start();
        

        }
        private void threadControl_Center()//thread method for mouse over control
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (lootVar == false)
                {
                    txt_Description.Text = "Push this to aim center.";
                }
                else
                {
                    string selectedItem = lbl_ItemCenter.Content.ToString();
                    var ItemDesc = (from x in db.itemLists where x.itemName.Equals(selectedItem) select x.itemDescription).FirstOrDefault();

                    txt_Description.Text = ItemDesc;
                }
            }));
        }
        private void mouseleave_center(object sender, RoutedEventArgs e)
        {
            txt_Description.Text = string.Empty;
        }
        private void mouseover_left(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(threadControl_Left); //thread for mouse over control
            thread.Start();
        }
        private void mouseleave_left(object sender, RoutedEventArgs e)
        {
            txt_Description.Text = string.Empty;
        }
        private void threadControl_Left()//thread method for mouse over control
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (lootVar == false)
                {
                    txt_Description.Text = "Push this to aim left.";
                }
                else
                {
                    string selectedItem = lbl_ItemLeft.Content.ToString();
                    var ItemDesc = (from x in db.itemLists where x.itemName.Equals(selectedItem) select x.itemDescription).FirstOrDefault();

                    txt_Description.Text = ItemDesc; ;
                }
            }));
        }
        private void mouseover_right(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(threadControl_Right); // thread for mouse over control
            thread.Start();
        }
        private void threadControl_Right() //thread method for mouse over control
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (lootVar == false)
                {
                    txt_Description.Text = "Push this to aim right.";
                }
                else
                {
                    string selectedItem = lbl_ItemRight.Content.ToString();
                    var ItemDesc = (from x in db.itemLists where x.itemName.Equals(selectedItem) select x.itemDescription).FirstOrDefault();

                    txt_Description.Text = ItemDesc;
                }
            }));
        }
        private void mouseleave_right(object sender, RoutedEventArgs e)
        {
            txt_Description.Text = string.Empty;
        }

        private void btn_Left_Click(object sender, RoutedEventArgs e)
        {
            StatData<int, int, int, int> playerStats = new StatData<int, int, int, int>();
            playerStats.health = Convert.ToInt32(lbl_Health.Content);// gets player data from form
            playerStats.damage = Convert.ToInt32(lbl_Damage.Content);// gets player data from form
            playerStats.defense = Convert.ToInt32(lbl_Defense.Content);// gets player data from form
            playerStats.energy = Convert.ToInt32(lbl_Energy.Content);// gets player data from form
            int opHealth = Convert.ToInt32(lbl_HealthOp.Content);
            int level = Convert.ToInt32(lbl_level.Content);
            if (lootVar == false && playerStats.health > 0)
            {
                string playerDirection = "left"; // sets string to left for combat mechanic
                combat(playerDirection); // activates combat mechanic
            }
            else if (lootVar)
            {

                string selectedItem = lbl_ItemLeft.Content.ToString();
                var ItemStats = from x in db.itemLists where x.itemName.Equals(selectedItem) select x;
                foreach (var item in ItemStats)// item is check for its stat affect to determine where to equip it.
                {
                    if (item.statAffect.Equals("health"))
                    {
                        lbl_HealthEquip.Content = item.itemName.ToString();
                        playerStats.health += Convert.ToInt32(item.statIncrease);
                        lbl_Health.Content = playerStats.health.ToString();
                    }
                    else if (item.statAffect.Equals("damage"))
                    {
                        int dmgIncrease = Convert.ToInt32(item.statIncrease);
                        lbl_DamageEquip.Content = item.itemName.ToString();
                        playerStats.damage += dmgIncrease;
                        lbl_Damage.Content = playerStats.damage.ToString();
                    }
                    else if (item.statAffect.Equals("defense"))
                    {
                        lbl_DefenseEquip.Content = item.itemName.ToString();
                        int defIncrease = Convert.ToInt32(lbl_Defense.Content);
                        playerStats.defense += Convert.ToInt32(item.statIncrease);
                        lbl_Defense.Content = playerStats.defense.ToString();
                    }
                    else if (item.statAffect.Equals("energy"))
                    {
                        int engyIncrease = Convert.ToInt32(item.statIncrease);
                        lbl_EnergyEquip.Content = item.itemName.ToString();
                        playerStats.energy += engyIncrease;
                        lbl_Energy.Content = playerStats.energy.ToString();
                    }
                }
                buttonStatus();
            }
        }

        private void btn_Right_Click(object sender, RoutedEventArgs e)
        {
            StatData<int, int, int, int> playerStats = new StatData<int, int, int, int>();
            playerStats.health = Convert.ToInt32(lbl_Health.Content);// gets player data from form
            playerStats.damage = Convert.ToInt32(lbl_Damage.Content);// gets player data from form
            playerStats.defense = Convert.ToInt32(lbl_Defense.Content);// gets player data from form
            playerStats.energy = Convert.ToInt32(lbl_Energy.Content);// gets player data from form
            int opHealth = Convert.ToInt32(lbl_HealthOp.Content);
            int level = Convert.ToInt32(lbl_level.Content);
            if (lootVar == false && playerStats.health > 0)
            {
                string playerDirection = "right"; // sets string to right for combat mechanic
                combat(playerDirection); // activates combat mechanic
            }
            else if (lootVar)
            {

                string selectedItem = lbl_ItemRight.Content.ToString();
                var ItemStats = from x in db.itemLists where x.itemName.Equals(selectedItem) select x;
                foreach (var item in ItemStats)// item is check for its stat affect to determine where to equip it.
                {
                    if (item.statAffect.Equals("health"))
                    {
                        lbl_HealthEquip.Content = item.itemName.ToString();
                        playerStats.health += Convert.ToInt32(item.statIncrease);
                        lbl_Health.Content = playerStats.health.ToString();
                    }
                    else if (item.statAffect.Equals("damage"))
                    {
                        int dmgIncrease = Convert.ToInt32(item.statIncrease);
                        lbl_DamageEquip.Content = item.itemName.ToString();
                        playerStats.damage += dmgIncrease;
                        lbl_Damage.Content = playerStats.damage.ToString();
                    }
                    else if (item.statAffect.Equals("defense"))
                    {
                        lbl_DefenseEquip.Content = item.itemName.ToString();
                        int defIncrease = Convert.ToInt32(lbl_Defense.Content);
                        playerStats.defense += Convert.ToInt32(item.statIncrease);
                        lbl_Defense.Content = playerStats.defense.ToString();
                    }
                    else if (item.statAffect.Equals("energy"))
                    {
                        int engyIncrease = Convert.ToInt32(item.statIncrease);
                        lbl_EnergyEquip.Content = item.itemName.ToString();
                        playerStats.energy += engyIncrease;
                        lbl_Energy.Content = playerStats.energy.ToString();
                    }
                }
                buttonStatus();
            }
        }
        private void btn_Begin_Click(object sender, RoutedEventArgs e)
        {
            // clears opponent stats for new opponent intitalization
            lbl_HealthOp.Content = "";
            lbl_DamageOp.Content = "";
            lbl_DefenseOp.Content = "";
            lbl_EnergyOp.Content = "";
            lbl_ItemCenter.Content = "";
            lbl_ItemLeft.Content = "";
            lbl_ItemRight.Content = "";
            
            

            int level = Convert.ToInt32(lbl_level.Content); //Starts at level 0
            level += 1;
            lbl_level.Content = level.ToString();
            if(level <= 1)
            {
                Player player = new Player();
                lbl_Health.Content = player.Health.ToString();
                lbl_Damage.Content = player.Damage.ToString();
                lbl_Defense.Content = player.Defense.ToString();
                lbl_Energy.Content = player.Energy.ToString();

            }


            Opponent opponent = new Opponent(level); // Intializes new opponent and sets stats
            lbl_HealthOp.Content = opponent.Health.ToString();
            lbl_DamageOp.Content = opponent.Damage.ToString();
            lbl_DefenseOp.Content = opponent.Defense.ToString();
            lbl_EnergyOp.Content = opponent.Energy.ToString();

            buttonStatus();//switches button status

            //sets loot to false so combat begins
            lootVar = false;

        }

        public void combat(string playerDirection) //Does combat mechanics on click of any button
        {

            StatData<int, int, int, int> playerStats = new StatData<int, int, int, int>();
            playerStats.health = Convert.ToInt32(lbl_Health.Content);// gets player data from form
            playerStats.damage = Convert.ToInt32(lbl_Damage.Content);// gets player data from form
            playerStats.defense = Convert.ToInt32(lbl_Defense.Content);// gets player data from form
            playerStats.energy = Convert.ToInt32(lbl_Energy.Content);// gets player data from form

            StatData<int, int, int, int> opponentStats = new StatData<int, int, int, int>();
            opponentStats.health = Convert.ToInt32(lbl_HealthOp.Content);// gets opponent data from form
            opponentStats.damage = Convert.ToInt32(lbl_DamageOp.Content);// gets opponent data from form
            opponentStats.defense = Convert.ToInt32(lbl_DefenseOp.Content);// gets opponent data from form
            opponentStats.energy = Convert.ToInt32(lbl_EnergyOp.Content);// gets opponent data from form

            string opChoice = atkDef(); //Gets opponents choice to defend or attack
            string opDirection = direction(); //Gets opponents direction choice

            string action = this.cmbox_Action.Text; // gets attack or defense selection from combobox

            if (action == "Attack" && playerStats.energy > 0) // if player chooses attack and has energy to do it
            {
                if (opChoice == "attack" && opDirection != playerDirection && opponentStats.energy > 0) //Gives conditions for damage to player and opponent
                {
                    playerStats.health = playerStats.health - (opponentStats.damage);
                    opponentStats.health = opponentStats.health - (playerStats.damage);
                    playerStats.energy -= 1;
                    opponentStats.energy -= 1;
                    txt_Description.Text = "You both attacked. Your health is now " + playerStats.health + " and your opponents health is " + opponentStats.health + ".";
                }
                else if (opChoice == "attack" && opponentStats.energy > 0)
                { // gives conditions for parry
                    playerStats.energy -= 1;
                    opponentStats.energy -= 1;
                    txt_Description.Text = "You both swung " + playerDirection + " and parried. Both lose energy but not health.";
                }
                else if (opChoice == "defense") // gives conditions if opponent blocks
                {
                    playerStats.health = Convert.ToInt32(playerStats.health - (.5 * opponentStats.defense));
                    playerStats.energy -= 1;
                    opponentStats.energy += 1;
                    txt_Description.Text = "Your opponent defended and didn't take damage, you lost energy and your health is now " + playerStats.health + ".";
                }
                else
                { // gives conditions if opponent has no energy to do anything
                    opponentStats.energy += 1;
                    playerStats.energy -= 1;
                    opponentStats.health = (opponentStats.health + opponentStats.defense) - playerStats.damage;
                    txt_Description.Text = "Your opponent is to tired to defend themselves, their health drops to " + opponentStats.health + ".";
                }

            }
            else if (action == "Defend") // if player choose defense
            {
                if (opChoice == "attack") // if opponents choice is attack 
                {
                    opponentStats.health -= Convert.ToInt32(( .5 * playerStats.defense));
                    playerStats.energy += 1;
                    opponentStats.energy -= 1;
                    txt_Description.Text = "You defended their attack putting their health at " + opponentStats.health + " and gained some energy.";
                }
                else // if opponent tries to attack 
                {
                    playerStats.energy += 1;
                    opponentStats.energy -= 1;
                    txt_Description.Text = "You both defeneded and gained some energy.";
                }
            }
            lbl_Health.Content = playerStats.health;// resets labels to new stats
            lbl_Energy.Content = playerStats.energy;// resets labels to new stats
            lbl_HealthOp.Content = opponentStats.health;// resets labels to new stats
            lbl_EnergyOp.Content = opponentStats.energy;// resets labels to new stats
            
            if (playerStats.health <= 0)
            {
                defeat();
            } else if (opponentStats.health <= 0)
            {
                int level = Convert.ToInt32(lbl_level.Content);
                lootVar = true;
                txt_Description.Text = "You defeated the enemy!";
                string selection = "None";
                string selection2 = "None";
                selection = loot(selection, selection2, level); //gets an item from loot method
                lbl_ItemCenter.Content = selection; //shows item in selection on GUI
                selection2 = loot(selection, selection2, level);//gets new item from loot method
                lbl_ItemLeft.Content = selection2;// sets item in selection on GUI
                string selection3 = loot(selection, selection2, level);//Gets third new item
                lbl_ItemRight.Content = selection3;//sets item in selection on GUI

            }
        }


        public string atkDef() //random choice: 70% chance to attack
        {
            var randomType = new Random();
            int x = randomType.Next(1, 10);
            if (x < 3)
            {
                return "defense";
            }
            else
            {
                return "attack";
            }
        }
        public string direction()// Random choice: Direction of attack
        {
            var randomDir = new Random();
            int num = randomDir.Next(1, 3);
            if (num == 1)
            {
                return "left";
            }
            else if (num == 2)
            {
                return "center";
            }
            else
            {
                return "right";
            }
        }
        public void defeat() // what happens if players health drops below 1
        {
            buttonStatus();
            txt_Description.Text = "You died, Want to try agian?";      
            lbl_level.Content = "0";
            lbl_HealthEquip.Content = ""; //clears items from list after death
            lbl_DamageEquip.Content = "";
            lbl_DefenseEquip.Content = "";
            lbl_EnergyEquip.Content = "";
        }
        public string loot(string selection, string selection2, int level)
        {
            List<string> items = new List<string>();
            
            var itemList = (from item in db.itemLists where (!item.itemName.Contains(selection)) && (!item.itemName.Contains(selection2)) select item).ToList(); //gets appropriate items for level
            Random random = new Random(); //new random
            foreach(var item in itemList)
            {
                if(item.itemLevel == level) // adds items to list based on level
                {
                    items.Add(item.itemName);
                }                                          
            }
            
            int index = random.Next(items.Count);


            return items[index];
        }
        public void buttonStatus()
        {
            if (!btn_Begin.IsEnabled) 
            {
                btn_Begin.Content = "Continue?";
                btn_Begin.IsEnabled = true; // sets reset button available
                btn_Center.IsEnabled = false;// sets lower buttons to unavailable
                btn_Left.IsEnabled = false;// sets lower buttons to unavailable
                btn_Right.IsEnabled = false;// sets lower buttons to unavailable
            }
            else
            {
                btn_Begin.Content = "";
                btn_Begin.IsEnabled = false; // sets reset button available
                btn_Center.IsEnabled = true;// sets lower buttons to unavailable
                btn_Left.IsEnabled = true;// sets lower buttons to unavailable
                btn_Right.IsEnabled = true;// sets lower buttons to unavailable
            }
            
        }

        private void btn_AddC_Click(object sender, RoutedEventArgs e)
        {
            itemList item = new itemList() // adds item to database
            {
                itemName = txt_Item.Text,
                statAffect = cmbox_StatChoice.SelectedIndex.ToString(),
                itemDescription = txt_Description.Text,
                itemLevel = chooseLevel(randomStat()),
                statIncrease = randomStat()

            };

            db.itemLists.InsertOnSubmit(item);
            try
            {
                db.SubmitChanges(); //submits new row
            }
            catch (Exception ex)
            {
                itemList itemRandom = new itemList() // if item could not be added, a random health potion is made
                {
                    itemName = "Rando" + randomStat(),
                    statAffect = cmbox_StatChoice.SelectedIndex.ToString(),
                    itemDescription = "No description for this item. It's a random potion to heal",
                    itemLevel = chooseLevel(randomStat()),
                    statIncrease = randomStat()

                };
            }
            db.SubmitChanges();
        }
        private int randomStat() //chooses a random stat for item
        {
            Random random = new Random();
            int stat = random.Next(0, 20);
            return stat;
        }
        private int chooseLevel(int stat) //uses random stat size to determine level item should be available
        {
            int itemLevel;

            if(stat <= 3)
            {
                itemLevel = 1;
            }else if(stat >= 3 && stat < 5)
            {
                itemLevel=2;
            } else if (stat >= 5 && stat < 7) {
                itemLevel=3;
            }
            else if (stat >= 7 && stat < 9)
            {
                itemLevel = 4;
            }
            else if (stat >= 9 && stat < 11)
            {
                itemLevel = 5;
            }
            else if (stat >= 11 && stat < 13)
            {
                itemLevel = 6;
            }
            else if (stat >= 13 && stat < 15)
            {
                itemLevel = 7;
            }
            else if (stat >= 15 && stat < 17)
            {
                itemLevel = 8;
            }
            else
            {
                itemLevel = 9;
            }           
            

            return itemLevel;
        }
    }


}
