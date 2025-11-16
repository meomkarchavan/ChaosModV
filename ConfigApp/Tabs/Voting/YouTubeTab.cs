using System.Windows;
using System.Windows.Controls;

namespace ConfigApp.Tabs.Voting
{
    public class YouTubeTab : Tab
    {
        private CheckBox? m_EnableYouTubeVoting = null;
        private TextBox? m_Port = null;

        private void SetElementsEnabled(bool state)
        {
            if (m_Port is not null)
                m_Port.IsEnabled = state;
        }

        protected override void InitContent()
        {
            PushNewColumn(new GridLength(340f));
            PushNewColumn(new GridLength(10f));
            PushNewColumn(new GridLength(150f));
            PushNewColumn(new GridLength(250f));
            PushNewColumn(new GridLength(10f));
            PushNewColumn(new GridLength());

            PushRowEmpty();
            PushRowEmpty();
            PushRowEmpty();
            m_EnableYouTubeVoting = new CheckBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Content = "Enable YouTube Voting (via Streamer.bot)"
            };
            m_EnableYouTubeVoting.Click += (sender, eventArgs) =>
            {
                SetElementsEnabled(m_EnableYouTubeVoting.IsChecked.GetValueOrDefault());
            };
            PushRowElement(m_EnableYouTubeVoting);
            PopRow();

            PushRowSpacedPair("Port", m_Port = new TextBox()
            {
                Width = 120f,
                Height = 20f
            });

            SetElementsEnabled(false);
        }

        public override void OnLoadValues()
        {
            if (m_EnableYouTubeVoting is not null)
            {
                m_EnableYouTubeVoting.IsChecked = OptionsManager.VotingFile.ReadValue("EnableVotingYouTube", false);
                SetElementsEnabled(m_EnableYouTubeVoting.IsChecked.GetValueOrDefault());
            }
            if (m_Port is not null)
                m_Port.Text = OptionsManager.VotingFile.ReadValue("YouTubeStreamerBotPort", 8080).ToString();
        }

        public override void OnSaveValues()
        {
            OptionsManager.VotingFile.WriteValue("EnableVotingYouTube", m_EnableYouTubeVoting?.IsChecked);
            if (int.TryParse(m_Port?.Text, out int port))
            {
                OptionsManager.VotingFile.WriteValue("YouTubeStreamerBotPort", port);
            }
        }
    }
}
