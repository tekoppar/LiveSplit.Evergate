using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using Tem.TemClass;

namespace LiveSplit.Evergate {
    public partial class Manager : Form {

        static public bool DESTROY = false;
        public MemoryManager Memory { get; set; }
        private Thread timerLoop;
        private bool useLivesplitColors = true;
        private bool noPause = false;
        private int lastFrameCount;
#if Manager
        public static void Main(string[] args) {
            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Manager());
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
#endif
        public Manager() {
            this.DoubleBuffered = true;
            InitializeComponent();
            this.lblNote.Dock = DockStyle.Fill;
            Memory = new MemoryManager();
            StartUpdateLoop();
            Text = "Evergate " + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        }
        public void StartUpdateLoop() {
            if (timerLoop != null) { return; }

            timerLoop = new Thread(UpdateLoop);
            timerLoop.IsBackground = true;
            timerLoop.Priority = ThreadPriority.AboveNormal;
            timerLoop.Start();
        }
        private void UpdateLoop() {
            bool lastHooked = false;
            while (timerLoop != null && Manager.DESTROY == false) {
                try {
                    bool hooked = Memory.HookProcess();
                    if (hooked) {
                        if (Manager.DESTROY == true && this.Disposing)
                            return;

                        try {
                            UpdateValues();
                        } catch (ObjectDisposedException ex) {
                            return;
                        }

                        if (Manager.DESTROY == true)
                            return;
                    }
                    if (lastHooked != hooked) {
                        lastHooked = hooked;
                        this.Invoke((Action)delegate () { lblNote.Visible = !hooked; });
                    }
                } catch { return; }
                Thread.Sleep(7);
            }
        }
        protected override void OnClosing(CancelEventArgs e) {
            if (Manager.DESTROY == true) {
                base.OnClosing(e);
                return;
            }

            Manager.DESTROY = true;
            while (this.timerLoop.IsAlive && this.timerLoop.Name != null) {

            }

            this.timerLoop = null;
            e.Cancel = true;
            CloseShit();
        }

        private void CloseShit() {
            this.Close();
        }
        public void UpdateValues() {
            if (Manager.DESTROY == true || this.Disposing) { return; }

            if (Manager.DESTROY == false && this.Disposing == false && this.timerLoop.IsAlive && this.InvokeRequired) {
                try {
                    this.Invoke(new Action(() => { UpdateValues(); }));
                    return;
                } catch (ObjectDisposedException ex) {
                    return;
                }
            }
            

            float FPS = Memory.FPS();
            int frameCount = Memory.FrameCount;
            PlayerScript player = Memory.GetPlayer();
            GameState state = Memory.GetGameState();
            Vector2 position = player.currPos.ToVector2();
            Vector2 speed = player.currVel.ToVector2();
            if (frameCount != lastFrameCount) {
                lastFrameCount = frameCount;
            }

            string scene = Memory.GetSceneName();
            if (string.IsNullOrEmpty(scene)) { scene = "N/A"; }

            lblScene.Text = $"Scene: {scene}";
            lblPos.Text = $"Pos: {position.X}, {position.Y}";
            lblSpeed.Text = $"Speed: {speed.X:0.000}, {speed.Y:0.000} ({!speed:0.000})";
            string noPuaseEnabled = noPause ? "On" : "Off";
            lblExtra.Text = "";
            lblFPS.Text = $"FPS: {FPS:0.0}";

            if (Memory.IsHooked == true) {
                lblCanDash.Text = $"Can Dash: {player.canDashNow}";
                string ins = player._inEMPZone_k__BackingField == true ? "EMP" : (player._inFierce_k__BackingField == true ? "Fierce" : (player._inGel_k__BackingField == true ? "Gel" : "Nothing"));
                lblIn.Text = $"In: {ins}";
                lblCanJump.Text = $"Can Jump: {player.canJump}";
            } else {
                lblCanDash.Text = "Can Dash: N/A";
                lblIn.Text = "In: N/A";
                lblCanJump.Text = "Can Jump: N/A";
            }
        }
        private void Manager_KeyDown(object sender, KeyEventArgs e) {
            if (!e.Control) { return; }

            if (e.KeyCode == Keys.L) {
                useLivesplitColors = !useLivesplitColors;
                if (useLivesplitColors) {
                    this.BackColor = Color.White;
                    this.ForeColor = Color.Black;
                } else {
                    this.BackColor = Color.Black;
                    this.ForeColor = Color.White;
                }
            }
        }
    }
}
