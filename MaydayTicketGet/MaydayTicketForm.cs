using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaydayTicketGet.MaydayTickets;
using Newtonsoft.Json;

namespace MaydayTicketGet
{
    public partial class MaydayTicket : Form
    {
        delegate void UpdateLableHandler(string text);

        public MaydayTicket()
        {
            InitializeComponent();
        }      

        private void MaydayTicket_Load(object sender, EventArgs e)
        {
            ExecuteTimeTxt.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            ResultView.Items.Clear();
            ThreadPool.QueueUserWorkItem(new WaitCallback(booking_mayday_tickets));
        }

        //透過Invoke，printReulst會以UI Thread執行
        //Print resut
        private void printResult(string text)
        {
            ResultView.Items.Add(text);
        }

        /// <summary>日處理執行區</summary>
        private void booking_mayday_tickets(object work)
        {
            UpdateLableHandler delegate_result = new UpdateLableHandler(printResult);
            try
            {
                TicketSetting setting = readSettings("tickets.json");
                if (setting == null)
                {
                    this.Invoke(delegate_result, "沒有設定訂票資訊，請key好再來");
                }

                var service = new GetMaydayTicketService(GetMaydayTicketService.MAYDAY_TICKEY_TEST_URL, setting);
                string msg = service.start();

                if (!string.IsNullOrWhiteSpace(msg))
                {
                    this.Invoke(delegate_result, msg);
                }

                service.end();
            }
            catch(Exception ex)
            {
                this.Invoke(delegate_result, ex.Message);
            }
        }

        private TicketSetting readSettings(string filePath)
        {
            try
            {
                string jsonStr = string.Empty;
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        jsonStr = sr.ReadToEnd();
                    }
                }


                TicketSetting setting = JsonConvert.DeserializeObject<TicketSetting>(jsonStr);
                return setting;
            }
            catch
            {
                return null;
            }
        }
    }
}
