using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Web;

namespace ServerApp
{
    public class Control
    {
        private Button btnSleep;

        NetworkStream stream;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var client = Connection.Instance.client;              
            SetContentView(Resource.Layout.Control);
            btnSleep = FindViewById<Button>(Resource.Id.btnSleep);

            btnSleep.Click += delegate
            {
                stream = client.GetStream();
                String s = "SLP2";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
            };
        }
    }
}
