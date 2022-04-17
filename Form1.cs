using System.Windows.Forms;

namespace sznék_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Game.UpdateGame(e);
            this.ScoreText = Game.score.ToString();
            Util.Wait(100);
            this.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Game.HandleKeys(sender, e);
        }
    }
}