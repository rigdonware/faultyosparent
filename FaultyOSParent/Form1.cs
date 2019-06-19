using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;

namespace FaultyOSParent
{
	[Serializable]
	class Message
	{
		public long timestamp;
		public int count;
	}

	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			const int size = 1024;
			const int length = 1024; 

			DateTime now = DateTime.UtcNow;
			long unixTime = ((DateTimeOffset)now).ToUnixTimeSeconds();

			using (var mmf = MemoryMappedFile.CreateFromFile(@"C:\Users\Rigdon\Documents\Visual Studio 2017\Projects\FaultyOSParent\FaultyOSParent\bin\Debug\counting.txt", 
															System.IO.FileMode.Open, "Counting", length))
			{
				MemoryMappedViewStream mmvStream = mmf.CreateViewStream(0, length);
				Message msg = new Message();
				msg.timestamp = unixTime;
				msg.count = 1;

				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(mmvStream, msg);
				mmvStream.Seek(0, System.IO.SeekOrigin.Begin);
			}

			Process childProcess = new Process();
			childProcess.StartInfo.FileName = "C:/Users/Rigdon/Documents/Visual Studio 2017/Projects/FaultyOSChild/FaultyOSChild/bin/Debug/FaultyOSChild.exe";
			childProcess.StartInfo.UseShellExecute = false;
			childProcess.StartInfo.CreateNoWindow = false;
			childProcess.Start();
		}
	}
}
