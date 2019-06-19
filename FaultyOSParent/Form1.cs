using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using SharedMemory;

namespace FaultyOSParent
{
	[Serializable]
	public struct Message
	{
		public string timestamp;
		public string count;
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
			//MemoryMappedFileSecurity security = new MemoryMappedFileSecurity();
			//security.AddAccessRule(new System.Security.AccessControl.AccessRule<MemoryMappedFileRights>("everyone", MemoryMappedFileRights.FullControl, System.Security.AccessControl.AccessControlType.Allow));

			////using (var mmf = MemoryMappedFile.CreateFromFile(@"C:\Users\Rigdon\Documents\Visual Studio 2017\Projects\FaultyOSParent\FaultyOSParent\bin\Debug\counting.txt", 
			////												System.IO.FileMode.Open, "Counting", length))
			////{
			////using (var mmf = MemoryMappedFile.CreateFromFile(@"C:\Users\Rigdon\Documents\temp\faultyosparent\FaultyOSParent\bin\Debug\counting.txt", FileMode.Open, "CountingMapForAssignment", length))
			////{
			//using (var mmf = MemoryMappedFile.CreateFromFile(
			//	new FileStream(@"C:\Users\Rigdon\Documents\temp\faultyosparent\FaultyOSParent\bin\Debug\counting.txt", FileMode.Create),
			//	"CountingMapFile",
			//	1024 * 1024,
			//	MemoryMappedFileAccess.ReadWrite,
			//	HandleInheritability.Inheritable,
			//	false))
			using (var producer = new SharedMemory.BufferReadWrite(name: "CountingBuffer", bufferSize: 1024))
			{

				//using (var accessor = mmf.CreateViewAccessor(0, length * 2))
				//{
				//	Message msg;
				//	msg.count = "1";
				//	msg.timestamp = unixTime.ToString();
				//	for (int i = 0; i < length; i++)
				//	{
				//		//accessor.Read(i, out msg);
				//		//if (msg.timestamp != unixTime)
				//		//	msg.count += 1;
				//		accessor.Write(i, ref msg);
				//	}
				//}


				//MemoryMappedViewAccessor fileMapView = mmf.CreateViewAccessor();
				//fileMapView.Write(0, 1000);
				//Container MyContainer = new Container();
				//fileMapView.Write<Container>(4, ref MyContainer);
				int count = 1;
				producer.Write<int>(ref count, 0);
				producer.Write<long>(ref unixTime, 500);


				//MemoryMappedViewStream mmvStream = mmf.CreateViewStream(0, length);
				//Message msg = new Message();
				//msg.timestamp = unixTime.ToString();
				//msg.count = "1";

				//BinaryFormatter formatter = new BinaryFormatter();
				//formatter.Serialize(mmvStream, msg);
				//mmvStream.Seek(0, System.IO.SeekOrigin.Begin);
			}

			//Process childProcess = new Process();
			////childProcess.StartInfo.FileName = "C:/Users/Rigdon/Documents/Visual Studio 2017/Projects/FaultyOSChild/FaultyOSChild/bin/Debug/FaultyOSChild.exe";
			//childProcess.StartInfo.FileName = "FaultyOSChild.exe";
			//childProcess.StartInfo.UseShellExecute = false;
			//childProcess.StartInfo.Arguments = "CountingMapFile";
			////childProcess.StartInfo.CreateNoWindow = false;
			//childProcess.Start();
			//while (true) ;
		}
	}
}
