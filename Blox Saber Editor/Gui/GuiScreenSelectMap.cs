using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using Color = System.Drawing.Color;

namespace Sound_Space_Editor.Gui
{
    class GuiScreenSelectMap : GuiScreen
    {
        private int logoTxt;
        private FontRenderer fr = EditorWindow.Instance.FontRenderer;
        private GuiButton _createMapButton;
        private GuiButton _loadMapButton;
        private GuiButton _lastMapButton;
        private GuiButton _importButton;
        private GuiButton _lastMapText;
        //private GuiButton _pasteDataButton;
        //private GuiButton _githubButton;

        public GuiScreenSelectMap() : base(0, 0, EditorWindow.Instance.ClientSize.Width, EditorWindow.Instance.ClientSize.Height)
        {
            using (var img = Properties.Resources.logo)
            {
                logoTxt = TextureManager.GetOrRegister("logo",img,true);
            }
            //if (File.Exists(Properties.Settings.Default.LastFile))
            //{
                _lastMapButton = new GuiButton(3, 0, 0, 256, 48, "OPTIONS");
                Buttons.Add(_lastMapButton);
            //}
            _createMapButton = new GuiButton(0, 0, 0, 256, 48, "CREATE NEW");
            _loadMapButton = new GuiButton(1, 0, 0, 256, 48, "EDIT LOCAL");
            _importButton = new GuiButton(2, 0, 0, 256, 48, "IMPORT MAP");
            _lastMapText = new GuiButton(-1, 0, 0, 256, 36, "Not yet implemented");
            /*_pasteDataButton = new GuiButton(4, 0, 0, 256, 36, "PASTE DATA");
            _githubButton = new GuiButton(5, 0, 0, 256, 36, "GITHUB LINK");
            _pasteDataButton.Visible = false;
            _githubButton.Visible = false;*/
            Buttons.Add(_createMapButton);
            Buttons.Add(_loadMapButton);
            Buttons.Add(_importButton);
            Buttons.Add(_lastMapText);
            //Buttons.Add(_pasteDataButton);
            //Buttons.Add(_githubButton);
            OnResize(EditorWindow.Instance.ClientSize);
        }

        public override void Render(float delta, float mouseX, float mouseY)
        {
            var size = EditorWindow.Instance.ClientSize;
            Glu.RenderTexturedQuad(ClientRectangle.Width / 2 - 400 / 2, ClientRectangle.Height / 2 - 300, 400, 400, 0, 0, 1, 1, logoTxt);
            base.Render(delta, mouseX, mouseY);
        }

        public override void OnResize(Size size)
        {
            ClientRectangle = new RectangleF(0, 0, size.Width, size.Height);
            if (!(_lastMapButton == null))
            {
                _createMapButton.ClientRectangle.X = ClientRectangle.Width / 2 - 518;
                _createMapButton.ClientRectangle.Y = ClientRectangle.Height * 0.8f;
                _loadMapButton.ClientRectangle.X = ClientRectangle.Width / 2 - 258;
                _loadMapButton.ClientRectangle.Y = ClientRectangle.Height * 0.8f;
                _importButton.ClientRectangle.X = ClientRectangle.Width / 2 + 2;
                _importButton.ClientRectangle.Y = ClientRectangle.Height * 0.8f;
                _lastMapButton.ClientRectangle.X = ClientRectangle.Width / 2 + 262;
                _lastMapButton.ClientRectangle.Y = ClientRectangle.Height * 0.8f;

                _lastMapText.ClientRectangle.X = _lastMapButton.ClientRectangle.X;
                _lastMapText.ClientRectangle.Y = _lastMapButton.ClientRectangle.Y - 36;

                Console.WriteLine(_lastMapText.Text);
                Console.WriteLine(_lastMapText.ClientRectangle.X);
                Console.WriteLine(_lastMapText.ClientRectangle.Y);
                Console.WriteLine(_lastMapText.Visible);
                Console.WriteLine(ClientRectangle.Width);
                Console.WriteLine(ClientRectangle.Height);


                /*_pasteDataButton.ClientRectangle.X = 123456;
                _pasteDataButton.ClientRectangle.Y = 123456;
                _githubButton.ClientRectangle.X = 123456;
                _githubButton.ClientRectangle.Y = 123456;*/
            } else
            {
                _createMapButton.ClientRectangle.X = ClientRectangle.Width / 2 - 518;
                _createMapButton.ClientRectangle.Y = ClientRectangle.Height * 0.8f;
                _loadMapButton.ClientRectangle.X = ClientRectangle.Width / 2 - 258;
                _loadMapButton.ClientRectangle.Y = ClientRectangle.Height * 0.8f;
                _importButton.ClientRectangle.X = ClientRectangle.Width / 2 + 2;
                _importButton.ClientRectangle.Y = ClientRectangle.Height * 0.8f;
            }
            /*_pasteDataButton.ClientRectangle.X = _importButton.ClientRectangle.X;
            _pasteDataButton.ClientRectangle.Y = _importButton.ClientRectangle.Y - 36;
            _githubButton.ClientRectangle.X = _importButton.ClientRectangle.X;
            _githubButton.ClientRectangle.Y = _importButton.ClientRectangle.Y - 72;*/
            base.OnResize(size);
        }
        protected override void OnButtonClicked(int id)
        {
            switch (id)
            {
                case 0:
                    EditorWindow.Instance.OpenGuiScreen(new GuiScreenCreate());
                    break;
                case 1:
                    var dialog = new OpenFileDialog
                    {
                        Title = "Select Map File",
                        Filter = "Text Documents (*.txt)|*.txt"
                    };
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        EditorWindow.Instance.LoadFile(dialog.FileName);
                    }
                    break;
                case 2:
                    var gclipboard = Clipboard.GetText();
                    WebClient wc = new WebClient();
                    try
                    {
                        while (true)
                        {
                            gclipboard = wc.DownloadString(gclipboard);
                        }
                    }
                    catch
                    {
                        EditorWindow.Instance.LoadMap(gclipboard, false);
                    }
                    break;
                case 3:
                    _lastMapText.Visible = true;
                    //EditorWindow.Instance.LoadFile(Properties.Settings.Default.LastFile);
                    break;
                /*case 4:
                    try
                    {
                        var clipboard = Clipboard.GetText();
                        EditorWindow.Instance.LoadMap(clipboard, false);
                    }
                    catch
                    {
                        return;
                    }
                    break;
                case 5:
                    try
                    {
                        var gclipboard = Clipboard.GetText();
                        WebClient wc = new WebClient();
                        var reply = wc.DownloadString(gclipboard);
                        EditorWindow.Instance.LoadMap(reply, false);
                    }
                    catch
                    {
                        MessageBox.Show("Couldn't read map data. File may be corrupted or copied incorrectly.");
                    }
                    break;*/
            }
            base.OnButtonClicked(id);
        }
    }
}
