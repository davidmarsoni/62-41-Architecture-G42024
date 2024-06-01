using DTO;

namespace WebApi.ExternalServices
{
    public sealed class PrinterSystemConnector
    {
        Boolean isConnected = false;

        // Singleton class
        private static readonly PrinterSystemConnector _printerSystemConnector = new PrinterSystemConnector();

        private PrinterSystemConnector()
        {
            isConnected = false;
            // try to connect to the printer server
            ConnectToPrinterServer();
        }

        public static PrinterSystemConnector getConnector() {
            return _printerSystemConnector;
        }

        public Boolean IsConnected()
        {
            return isConnected;
        }

        public async Task<Boolean> ConnectToPrinterServer() { 
            System.Threading.Thread.Sleep(2800); // Simulate a long running task
            isConnected = true;
            return isConnected;
        }

        public async Task<Boolean> PushTransactionOntoPrinterServer(TransactionHistoryDTO obj)
        {
            System.Threading.Thread.Sleep(2800); // Simulate a long running task
            System.Console.WriteLine("Transaction pushed to printer server: " + obj.ConversionName + " " + obj.ConversionValue);
            return true;
        }
    }
}
