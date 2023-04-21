﻿using OWOGame;
using owoMedia.genericComponents.pageDefinition;
using owoMedia.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using owoMedia.sensationPlayer;
using owoMedia.videoViewer.data;
using owoMedia.websocket;
using System.Runtime.Remoting.Messaging;

namespace owoMedia.videoViewer.components.pages {
    public partial class VideoViewerPage : UserControlPage {

        string videoId;

        public VideoViewerPage(string videoId){
            this.videoId = videoId;
            InitializeComponent();
        }

        public override void Init() {
            base.Init();
            string html = Resources.videoViewer;
            html = html.Replace("$VIDEO_ID$", videoId);
            html = html.Replace("$WS_PORT$", OwoMedia.Instance.Config.Port.ToString());
            html = html.Replace("$WS_ROUTE$", WsVideoViewerBehavior.Route);
            //this.webVideo.DocumentText = html;

            string testName = "videoViewerTest.html";
            if (File.Exists(testName)) {
                File.Delete(testName);
            }
            using (FileStream fs = File.Create(testName)) {
                Byte[] title = new UTF8Encoding(true).GetBytes(html);
                fs.Write(title, 0, title.Length);
            }
            System.Diagnostics.Process.Start(testName);
        }



        WsVideoData.StateEnum lastState = WsVideoData.StateEnum.Unstarted;
        public List<string> OnWsMessage(WsVideoData dto) {
            if (this.lblState.InvokeRequired) {
                return (List<string>)this.lblState.Invoke(
                  new Func<List<string>>(() => ActualOnWsMessage(dto))
                );
            } else {
                return ActualOnWsMessage(dto);
            }
            
        }

        public delegate void OnWsMessageDelegate(WsVideoData dto);
        public OnWsMessageDelegate wsMessageDelegate;
        public List<string> ActualOnWsMessage(WsVideoData dto) {

            List<string> messages = new List<string>();

            if (dto.videoId != this.videoId) {
                messages.Add("IdError");
                return messages;
            }

            if (lastState == WsVideoData.StateEnum.Unstarted) {
                messages.Add("Play");
            }

            switch (dto.useCase) {
                case "init":
                    messages.Add("Connected");
                    lastState = WsVideoData.StateEnum.Unstarted;
                    break;
                case "state":
                    OnStateChange(dto);
                    break;
                case "time":
                    // general Check;
                    break;
                default:
                    Console.WriteLine("Unknown Usecase: " + dto.useCase);
                    break;
            }

            OnDetectTime(dto);

            return messages;
        }

        SyncableStopwatch stopWatch = new SyncableStopwatch();


        bool sync = false;
        private void btnSync_Click(object sender, EventArgs e) {
            sync = true;
        }

        private void OnDetectTime(WsVideoData dto) {
            if (sync) {
                sync = false;
                stopWatch.SyncTime(dto.timeStamp);
            }

            this.lblTimeWs.Text = dto.timeStamp.ToString();
            this.lblTime.Text = stopWatch.GetSyncedSeconds().ToString();
            this.lblDiff.Text = (dto.timeStamp - stopWatch.GetSyncedSeconds()).ToString();

            TimeSpan t = TimeSpan.FromSeconds(stopWatch.GetSyncedSeconds());
            string formatedTime;
            if (t.Hours > 0) {
                formatedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            } else {
                formatedTime = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            }
            lblTimeTidy.Text = formatedTime;

        }

        private void OnStateChange(WsVideoData dto) {
            WsVideoData.StateEnum newState = (WsVideoData.StateEnum)Int16.Parse(dto.value);

            if (lastState != WsVideoData.StateEnum.Playing && newState == WsVideoData.StateEnum.Playing) {
                stopWatch.Start();
            } else if (lastState == WsVideoData.StateEnum.Playing && newState != WsVideoData.StateEnum.Playing) {
                stopWatch.Stop();
            }

            lastState = newState;
            this.lblState.Text = lastState.ToString();
        }

        private void btnOwoShoot_Click(object sender, EventArgs e) {

            MicroSensation ball = SensationsFactory.Create(100, 0.1f, 100, 0, 0, 0);
            Sensation softBall = ball.WithMuscles(Muscle.Pectoral_R.WithIntensity(50));

            OWO.Send(softBall);
        }

        public override void OnLeave() {
            base.OnLeave();
            // stops Sensations

        }
    }
}
