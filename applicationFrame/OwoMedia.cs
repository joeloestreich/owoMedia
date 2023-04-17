﻿using OWOGame;
using owoMedia.config.data;
using owoMedia.home.components.pages;
using owoMedia.videoViewer.components.pages;
using owoMedia.welcome.components.pages;
using owoMedia.websocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace owoMedia.genericComponents.pageDefinition {
    public sealed partial class OwoMedia : Form {

        private static OwoMedia _instance;
        public static OwoMedia Instance { 
            get { 
                if (_instance == null) {
                    _instance = new OwoMedia();
                }
                return _instance;
            }
        }

        private OwoMedia() {
            InitializeComponent();
        }


        public Config Config;
        public UserControlPage CurrentPage { get; set; }

        private void OwoMedia_Load(object sender, EventArgs e) {
            Config = null; // load here
            if (Config == null) {
                Config = new Config();
            }

            UserControlPage initialPage;
            switch (Config.DefaultPage) {
                case Config.DefaultPageEnum.Welcome:
                    initialPage = new WelcomePage();
                    break;
                case Config.DefaultPageEnum.Home:
                    initialPage = new HomePage();
                    break;
                case Config.DefaultPageEnum.Search:
                    initialPage = new VideoSelectorPage();
                    initialPage = new VideoSelectorPage();
                    break;
                case Config.DefaultPageEnum.Edit:
                    initialPage = new WelcomePage();
                    break;
                default:
                    Config.DefaultPage = Config.DefaultPageEnum.Welcome;
                    initialPage = new WelcomePage();
                    break;
            }


            StartUp(initialPage);
        }
        private void OwoMedia_FormClosing(object sender, FormClosingEventArgs e) {
            Disconnect();
        }

        private void StartUp(UserControlPage initialPage) {
            // Design
            this.Size = Config.Size;
            Connect();
            NavigateTo(initialPage);
        }

        public void NavigateTo(UserControlPage page) {
            if (CurrentPage != null) {
                CurrentPage.OnLeave();
                this.Controls.Remove(CurrentPage);
            }

            page.Init();
            CurrentPage = page;
            CurrentPage.Dock = DockStyle.Fill;
            this.Controls.Add(page);
        }

        private void Reload() {
            Disconnect();
            Connect();
            this.CurrentPage.Reload();
        }

        private void Connect() {
            if (Config.OwoIp == null) {
                OWO.AutoConnect();
            } else {
                OWO.Connect(Config.OwoIp);
            }

            WebSocketService.StartSocketServer();
        }

        private void Disconnect() {
            WebSocketService.StopSocketServer();
            OWO.Disconnect();
        }
    }
}