using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bds.sql{
    public enum queryResult : int {
        ConnectionSuccess = 1,
        ConnectionFail = 0,
        TransactionSuccess = 1,
        TransactionFail = 0,
        CommandSuccess = 1,
        CommandFail = 0,
        NotRun = -1

    }



}
