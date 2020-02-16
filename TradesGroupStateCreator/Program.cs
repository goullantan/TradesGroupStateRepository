using ConfigurationHelper.Contracts;
using log4net;
using System;
using System.Reflection;
using TradesGroupStateCreator.Contracts;

namespace TradesGroupStateCreator
{
    public class Program
    {
        private const string ConfigTemplateMethods = "TemplateMethods";
        private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Logger.Info("*** Starting trades treatment ***");
            try
            {
                using (var bootstrapper = new Bootstrapper())
                {
                    bootstrapper.Build();//Register all needed instances

                    var configurationDataProvider = bootstrapper.Resolve<IConfigurationDataProvider>();

                    //Pattern : write the instance name of your choice for each steps in the App.config file (TemplateMethods section)
                    //Example : want to create an xls report instead of csv, write a class for this and implement ITradesGroupReportGenerator interface
                    //          then add the name of you class in the ReportGeneratorInstance value

                    //Input trades
                    var inputTradesProviderInstance = configurationDataProvider.GetValueFromSectionAndKey(ConfigTemplateMethods, "InputTradesProviderInstance");
                    var inputTradesProvider = bootstrapper.Resolve<IInputTradesProvider>(inputTradesProviderInstance);
                    var inputTrades = inputTradesProvider.GetInputTrades();
                    //Trades aggregation
                    var tradesAggregatorInstance = configurationDataProvider.GetValueFromSectionAndKey(ConfigTemplateMethods, "TradesAggregatorInstance");
                    var tradesAggregator = bootstrapper.Resolve<ITradesAggregator>(tradesAggregatorInstance);
                    var aggregatedTrades = tradesAggregator.AggregateInputTrades(inputTrades);
                    //State attribution
                    var stateAttributorInstance = configurationDataProvider.GetValueFromSectionAndKey(ConfigTemplateMethods, "StatusAttributorInstance");               
                    var stateAttributor = bootstrapper.Resolve<ITradesGroupStateAttributor>(stateAttributorInstance);
                    var aggregatedTradesWithState = stateAttributor.AttributeState(aggregatedTrades);
                    //Report generation
                    var reportGeneratorInstance = configurationDataProvider.GetValueFromSectionAndKey(ConfigTemplateMethods, "ReportGeneratorInstance");
                    var reportGenerator = bootstrapper.Resolve<ITradesGroupReportGenerator>(reportGeneratorInstance);
                    reportGenerator.GenerateCsvReport(aggregatedTradesWithState);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
            Logger.Info("End of process.");
        }
    }
}
