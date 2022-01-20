using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Shiny;
using Shiny.BluetoothLE;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeGrillXam.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly IBleManager _bleManager;
        private const string NOTIFY_CHAR_UUID = "0000ffe4-0000-1000-8000-00805f9b34fb";
        private const string NOTIFY_SERVICE_UUID = "0000ffe0-0000-1000-8000-00805f9b34fb";


        public AboutViewModel()
        {
            _bleManager = ShinyHost.Resolve<IBleManager>();

            Title = "About";
            OpenWebCommand = new Command(async () =>
            {
                if (_bleManager.IsScanning)
                {
                    _bleManager.StopScan();
                }

                ScanForDevices();
            });
        }

        public ICommand OpenWebCommand { get; }

        private void ScanForDevices()
        {
            _bleManager.Scan().Subscribe(async scanResult =>
            {
                if ((scanResult.Peripheral?.Name?.Contains("WeGrill")).GetValueOrDefault())
                {
                    Console.WriteLine("Found WeGrill device, connecting...");
                    var peripheral = scanResult.Peripheral;
                    await peripheral.ConnectAsync();

                    peripheral.Notify(NOTIFY_SERVICE_UUID, NOTIFY_CHAR_UUID).Subscribe(notification =>
                    {
                        string message = BitConverter.ToString(notification.Data).Replace("-", string.Empty);
                        if (message.Length == 8)
                        {
                            if (message.StartsWith("02"))
                            {
                                int temperature = int.Parse(message.Substring(4), System.Globalization.NumberStyles.HexNumber);
                                Console.WriteLine($"Got temperature {temperature}");
                            }
                        }
                    });
                }
            });
        }
    }
}
