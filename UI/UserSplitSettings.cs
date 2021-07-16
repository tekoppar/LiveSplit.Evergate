using System;
using System.ComponentModel;
using System.Windows.Forms;
namespace LiveSplit.Evergate {
    public partial class UserSplitSettings : UserControl {
        public Split UserSplit;
        private object DefaultValue = null;
        private bool isDragging = false;
        private int mX = 0;
        private int mY = 0;
        private bool isLoading = false;
        public UserSplitSettings() {
            InitializeComponent();
        }
        public void UpdateControls(bool updateType = false, bool updateValue = true) {
            if (updateType) {
                isLoading = true;
                cboType.DataSource = Utility.GetEnumList<SplitType>();
                cboType.SelectedIndex = -1;
                cboType.SelectedIndex = -1;
                isLoading = false;
                cboType.SelectedValue = UserSplit.Type;
            }

            isLoading = true;
            if (updateValue) {
                switch (UserSplit.Type) {
                    case SplitType.LevelCompleted:
                        cboValue.DataSource = Utility.GetEnumList<SplitLevel>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitLevel>(UserSplit.Value);
                        break;
                    case SplitType.LevelStart:
                        cboValue.DataSource = Utility.GetEnumList<SplitLevel>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitLevel>(UserSplit.Value);
                        break;
                    case SplitType.SelectedBook:
                        cboValue.DataSource = Utility.GetEnumList<SplitBook>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitBook>(UserSplit.Value);
                        break;
                    default:
                        txtValue.Text = UserSplit.Value;
                        break;
                }
            }
            lblSegment.Text = UserSplit.Name;
            isLoading = false;
        }
        private void cboType_SelectedIndexChanged(object sender, EventArgs e) {
            if (cboType.SelectedValue == null || isLoading) { return; }

            SplitType nextControlType = (SplitType)cboType.SelectedValue;
            if (nextControlType == SplitType.ManualSplit || nextControlType == SplitType.GameStart || nextControlType == SplitType.GameEnd) {
                txtValue.Visible = false;
                cboValue.Visible = false;
                UserSplit.Value = string.Empty;
            } else if (nextControlType == SplitType.Hitbox) {

                if (nextControlType != UserSplit.Type) {
                    UserSplit.Value = "1";
                }
                txtValue.Width = 54;
                txtValue.Visible = true;
                cboValue.Visible = false;
            } else if (nextControlType == SplitType.Hitbox) {
                if (nextControlType != UserSplit.Type) {
                    UserSplit.Value = "X, Y, Width, Height";
                }
                txtValue.Width = 202;
                txtValue.Visible = true;
                cboValue.Visible = false;
            } else if (nextControlType == SplitType.Hitbox) {
                if (nextControlType != UserSplit.Type) {
                    UserSplit.Value = "34543|11226";
                }
                txtValue.Width = 202;
                txtValue.Visible = true;
                cboValue.Visible = false;
            } else if (nextControlType == SplitType.Hitbox) {
                if (nextControlType != UserSplit.Type) {
                    UserSplit.Value = "UberGroup, UberId, Comparison, Value | 8246,62310,>,500.0";
                }
                txtValue.Width = 202;
                txtValue.Visible = true;
                cboValue.Visible = false;
            } else {
                if (nextControlType != UserSplit.Type) {
                    switch (nextControlType) {
                        case SplitType.LevelCompleted: DefaultValue = SplitLevel.C11; break;
                        case SplitType.LevelStart: DefaultValue = SplitLevel.C11; break;
                        case SplitType.SelectedBook: DefaultValue = SplitBook.ChinaLevel1; break;
                    }
                    UserSplit.Value = DefaultValue.ToString();
                }
                txtValue.Visible = false;
                cboValue.Visible = true;
            }
            UserSplit.Type = nextControlType;

            UpdateControls();
        }
        private void cboType_Validating(object sender, CancelEventArgs e) {
            if (cboType.SelectedIndex < 0 && !isLoading) {
                cboType.SelectedValue = SplitType.ManualSplit;
            }
        }
        private void cboValue_SelectedIndexChanged(object sender, EventArgs e) {
            if (cboValue.Visible && cboValue.SelectedItem != null && !isLoading) {
                UserSplit.Value = cboValue.SelectedValue.ToString();
            }
        }
        private void cboValue_Validating(object sender, CancelEventArgs e) {
            if (cboValue.Visible && cboValue.SelectedIndex < 0) {
                cboValue.SelectedValue = DefaultValue;
            }
        }
        private void txtValue_Validating(object sender, CancelEventArgs e) {
            if (txtValue.Visible) {
                UserSplit.Value = txtValue.Text;
            }
        }
        private void picHandle_MouseMove(object sender, MouseEventArgs e) {
            if (!isDragging) {
                if (e.Button == MouseButtons.Left) {
                    int num1 = mX - e.X;
                    int num2 = mY - e.Y;
                    if (((num1 * num1) + (num2 * num2)) > 20) {
                        DoDragDrop(this, DragDropEffects.All);
                        isDragging = true;
                        return;
                    }
                }
            }
        }
        private void picHandle_MouseDown(object sender, MouseEventArgs e) {
            mX = e.X;
            mY = e.Y;
            isDragging = false;
        }
    }
}