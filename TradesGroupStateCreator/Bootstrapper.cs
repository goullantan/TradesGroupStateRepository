using ConfigurationHelper;
using ConfigurationHelper.Contracts;
using CsvFileManager;
using CsvFileManager.Contracts;
using IocHelper;
using TradesGroupStateCreator.Contracts;
using XmlSerializer;
using XmlSerializer.Contracts;

namespace TradesGroupStateCreator
{
    public class Bootstrapper : BootstrapperBase
    {      
        public override void Build()
        {
            RegisterSingleton<IConfigurationDataProvider, ConfigurationDataProvider>();
            RegisterSingleton<IXmlDocumentReader, XmlDocumentReader>();
            RegisterSingleton<IInputXmlFileWrapper, InputXmlFileWrapper>();
            RegisterSingleton<IXmlDocumentSerializer, XmlDocumentSerializer>();
            RegisterSingleton<IOutputCsvFileWrapper, OutputCsvFileWrapper>();
            RegisterSingleton<ICsvFileCreator, CsvFileCreator>();

            //Template methods instances
            RegisterSingleton<IInputTradesProvider, InputTradesProvider>(serviceKey:nameof(InputTradesProvider));
            RegisterSingleton<ITradesAggregator, TradesAggregator>(serviceKey: nameof(TradesAggregator));
            RegisterSingleton<ITradesGroupStateAttributor, TradesGroupStateAttributor>(serviceKey: nameof(TradesGroupStateAttributor));            
            RegisterSingleton<ITradesGroupReportGenerator, TradesGroupReportGenerator>(serviceKey: nameof(TradesGroupReportGenerator));
        }
    }
}
