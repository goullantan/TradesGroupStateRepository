using System.Collections.Generic;
using System.Linq;

namespace TradesGroupStateCreator.Contracts
{
    public interface ITradesGroupStateAttributor
    {
        /// <summary>
        /// Attribute State
        /// </summary>
        /// <param name="trades"></param>
        /// <returns></returns>
        IOrderedEnumerable<TradesGroupModel> AttributeState(IEnumerable<TradeModel> trades);
    }
}
