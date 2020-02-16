namespace ConfigurationHelper.Contracts
{
    public interface IConfigurationDataProvider
    {
        /// <summary>
        /// Get Value FromSection And Key
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValueFromSectionAndKey(string sectionName, string key);
    }
}
