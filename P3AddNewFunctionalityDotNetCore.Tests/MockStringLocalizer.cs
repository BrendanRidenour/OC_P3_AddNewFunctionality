using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class MockStringLocalizer<T> : IStringLocalizer<T>
    {
        public LocalizedString this[string name] => new LocalizedString(name, value: name);

        #region Unnecessary
        public LocalizedString this[string name, params object[] arguments] => throw new System.NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new System.NotImplementedException();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}