using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {

        }

        public void conectar(string texto)
        {
            try
            {
                IamAuthenticator authenticator = new IamAuthenticator(apikey: "_DPGYFyIP4aUykVS53B4JFawtz261nvV4l9GGLUuN4eb");

                AssistantService assistant = new AssistantService("2021-06-14", authenticator);
                assistant.SetServiceUrl("https://api.eu-de.assistant.watson.cloud.ibm.com");

                assistant.DisableSslVerification(true);

                //Creem la sessio
                var result = assistant.CreateSession(assistantId: "7ca45e38-0155-4856-8c0b-86e574c514b6");
                //Escribimos la SessionID
                Console.WriteLine(result.Response);
                //Extraemos solo el SessionID de la respuesta
                string sessionID = result.Response.Substring(20, 36);
                //Escribimos el sessionID Limpio
                Console.WriteLine("Session ID: " + sessionID);

                //Mandamos un mensaje de prueba
                var result2 = assistant.Message(
                    assistantId: "7ca45e38-0155-4856-8c0b-86e574c514b6",
                    sessionId: sessionID,
                    input: new MessageInput()
                    {
                        Text = texto
                    }
                 );
                    

                Console.WriteLine(result2.Response);
                txbConvers.Text = result2.Response;

            }
            catch (ServiceResponseException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            //Llamamos al metodo conectar
            conectar(txtEntrada.Text);
        }


    }
}
