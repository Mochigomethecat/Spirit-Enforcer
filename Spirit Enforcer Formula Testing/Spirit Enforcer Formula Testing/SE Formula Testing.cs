using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spirit_Enforcer_Formula_Testing
{
    public partial class lblTotalBase : Form
    {
        public lblTotalBase()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormulaTesting_Load(object sender, EventArgs e)
        {
            //default output
            string formulaInfo = "LevelDisplay  = (LevelActual * 10);"+ Environment.NewLine + Environment.NewLine +
            "Health = (base/4) + (Vitality * DevVit) + (Strength * DevStr) * (DevHp) * LevelActual;" + Environment.NewLine + Environment.NewLine +
            "Spirit = (base/4) + (HpMax / 2) + (Vitality * DevVit) * (DevSp) * LevelActual;" + Environment.NewLine + Environment.NewLine +
            "Stamina = (base/4) + (Endurance * DevEnd) + (Strength * DevStr) * (DevSt) * LevelActual;" + Environment.NewLine + Environment.NewLine +
            "DamAttack  = (base/4) + (Attack * DevAtt) + rand((levelDisplay/2) _ levelDisplay) * LevelActual;" + Environment.NewLine + Environment.NewLine +
            "DamDefense = (base/4) + (Defense * DevDef) + rand((levelDisplay/2) _ levelDisplay) * LevelActual;" + Environment.NewLine + Environment.NewLine +
            "SpAttack  = (base/4) + (SPAttack * DevSAtt) + rand((levelDisplay/2) _ levelDisplay) * LevelActual;" + Environment.NewLine + Environment.NewLine +
            "SpDefense = (base/4) + (SPDefense * DevSDef) + rand((levelDisplay/2) _ levelDisplay) * LevelActual;" + Environment.NewLine + Environment.NewLine +
            "Dash = Speed * 2.25; //dash multiplier subject to change";

            lblBackgroundInfo.Text = formulaInfo;

            //load
            comboElemental.SelectedIndex = 0;
            comboAttackType1.SelectedIndex = 0;
            comboAttackType2.SelectedIndex = 0;
            comboWeaponType.SelectedIndex = 0;
            radHuman.Checked = true;

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            //input variables
            int levelDisplay   = int.Parse(txtLevelInput.Text);
            int baseHp         = int.Parse(txtBaseHealth.Text);
            int baseSp         = int.Parse(txtBaseSpirit.Text);
            int baseSt         = int.Parse(txtBaseStamina.Text);
            int baseAtt        = int.Parse(txtBaseAttack.Text);
            int baseDef        = int.Parse(txtBaseDefense.Text);
            int baseSAtt       = int.Parse(txtBaseSAttack.Text);
            int baseSDef       = int.Parse(txtBaseSDefense.Text);

            //overdrive check
            if(chkOverdrive.Checked)
            {
                comboElemental
            }

            //enemy stats
            int enBaseHp  = int.Parse(txtEnHp.Text);
            int enBaseSp  = int.Parse(txtEnSp.Text);
            int enBaseSt  = int.Parse(txtEnSt.Text);
            int enBaseAtt = int.Parse(txtEnAtt.Text);
            int enBaseDef = int.Parse(txtEnDef.Text);
            int enBaseSat = int.Parse(txtEnSatt.Text);
            int enBaseSdf = int.Parse(txtEnSdef.Text);
            int enHp, enSp, enSt, enAtt, enDef, enSAtt, enSDef;
            int enBleed = 0;

            //deviation inputs
            double devHp       = double.Parse(txtHealthDeviation.Text);
            double devSp       = double.Parse(txtSpiritDeviation.Text);
            double devSt       = double.Parse(txtStaminaDeviation.Text);
            double devAtt      = double.Parse(txtAttackDeviation.Text);
            double devDef      = double.Parse(txtDefenseDeviation.Text);
            double devSAtt     = double.Parse(txtSAttackDeviation.Text);
            double devSDef     = double.Parse(txtSDefenseDeviation.Text);
            double devStr      = double.Parse(txtStrengthDeviation.Text);
            double devVit      = double.Parse(txtVitalityDeviation.Text);
            double devEnd      = double.Parse(txtEnduranceDeviation.Text);

            //modifiers
            int strength       = int.Parse(txtStrength.Text);
            int vitality       = int.Parse(txtVitality.Text);
            int endurance      = int.Parse(txtEndurance.Text);

            //combobox inputs
            string elementType = comboElemental.SelectedItem.ToString();
            string weaponType  = comboWeaponType.SelectedItem.ToString();
            string attribute1  = comboAttackType1.SelectedItem.ToString();
            string attribute2  = comboAttackType2.SelectedItem.ToString();

            //battle stats
            int attack = 0;
            int bleed  = 0;

            //formulas
            double levelActual = (levelDisplay / 10);      
            double health      = ((baseHp/4) + (vitality * devVit) + (strength * devStr) * (devHp) * levelActual);
                double HpMax   = Math.Floor(health);
                double HpNow   = HpMax;
            double spirit      = ((baseSp/4) + (HpMax / 2) + (vitality * devVit) * (devSp) * levelActual);
                double SpMax   = Math.Floor(spirit);
                double SpNow   = SpMax;
            double stamina     = ((baseSt/4) + (endurance * devEnd) + (strength * devStr) * levelActual);
                double StMax   = Math.Floor(stamina);
                double StNow   = StMax;
            double damAttack   = Math.Floor((baseAtt/4) + (1.3));
            double damDefense  = Math.Floor(1.1);
            double spirAttack  = Math.Floor(1.1);
            double spirDefense = Math.Floor(1.1);
            double devTotal    = devHp + devSp + devSt +devAtt +devDef + devSAtt + devSDef + devStr + devVit + devEnd;

            //generate random seed
            Random levelSeed = new Random();
            int randomModifier = levelSeed.Next((levelDisplay/2), (levelDisplay+1));

            //enemy type formulas
            if (radHuman.Checked)
            {
                enHp   = (enBaseHp  / 4);
                enSp   = ((enBaseSp / 4) + (enHp / 2));
                enSt   = (enBaseSt  / 4);
                enAtt  = (enBaseAtt / 4);
                enDef  = (enBaseDef / 4);
                enSAtt = (enBaseSat / 4);
                enSDef = (enBaseSdf / 4);
            }
            else if (radFiend.Checked)
            {
                //enemy formulas
                enSp   = (enBaseHp  / 2) * enBaseSat * enBaseSdf;
                enHp   = (enBaseHp  * (enSp/2));
                enSt   = (enBaseSt  / 4);
                enAtt  = (enBaseAtt / 4);
                enDef  = (enBaseDef / 4);
                enSAtt = (enBaseSat / 4);
                enSDef = (enBaseSdf / 4);
            }
            else if (radMachine.Checked)
            {
                enHp   = (int)(enBaseHp * 1.5);
                enSp   = (enBaseSp  * 0);
                enSt   = (enBaseSt  * 0);
                enAtt  = (enBaseAtt / 4);
                enDef  = (enBaseDef / 4);
                enSAtt = (enBaseSat * 0);
                enSDef = (enBaseSdf * 0);
            }
            else
            {
                enHp   = (0);
                enSp   = (0);
                enSt   = (0);
                enAtt  = (0);
                enDef  = (0);
                enSAtt = (0);
                enSDef = (0);
            }

            //validity conditions
            if (devTotal != 5)
            {
                lblDeviationUsed.Text = devTotal.ToString();
                MessageBox.Show("Deviation is not equal to 5");
                return;
            }
            else
            {
                lblDeviationUsed.Text = devTotal.ToString();
            }

            ///outputs
            //calc stats
            lblRandomSeed.Text    = randomModifier.ToString();
            lblHealth.Text        = HpMax.ToString();
            lblSpirit.Text        = SpMax.ToString();
            lblStamina.Text       = StMax.ToString();
            lblAttack.Text        = damAttack.ToString();
            lblDefense.Text       = damDefense.ToString();
            lblSAttack.Text       = spirAttack.ToString();
            lblSDefense.Text      = spirDefense.ToString();
            //battle life
            lblPlayerHp.Text      = HpNow.ToString();
            lblPlayerSp.Text      = SpNow.ToString();
            lblPlayerBleed.Text   = bleed.ToString();
            lblPlayerStamina.Text = StNow.ToString();
            lblEnemyHp.Text       = enHp.ToString();
            lblEnemySp.Text       = enSp.ToString();
            lblEnemyBleed.Text    = enBleed.ToString();
            lblEnemyStamina.Text  = enSt.ToString();
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}









