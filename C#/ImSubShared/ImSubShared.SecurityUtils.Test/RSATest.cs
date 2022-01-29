using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImSubShared.SecurityUtils.Test
{
    [TestClass]
    public class RSATest
    {
        private string modulus = "6lq9MQ-q6hcxr7kOUp-tHlHtdcDsVLwVIw13iXUCvuDOeCi0VSuxCCUY6UmMjy53dX00ih2E4Y4UvlrmmurK0eG26b-HMNNAvCGsVXHU3RcRhVoHDaOwHwU72j7bpHn9XbP3Q3jebX6KIfNbei2MiR0Wyb8RZHE-aZhRYO8_-k9G2GycTpvc-2GBsP8VHLUKKfAs2B6sW3q3ymU6M0L-cFXkZ9fHkn9ejs-sqZPhMJxtBPBxoUIUQFTgv4VXTSv914f_YkNw-EjuwbgwXMvpyr06EyfImxHoxsZkFYB-qBYHtaMxTnFsZBr6fn8Ha2JqT1hoP7Z5r5wxDu3GQhKkHw";
        private string exponent = "AQAB";
        private string signedString = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxNDk0MzkxNDQiLCJ1cGRhdGVkX2F0IjoiMDAwMS0wMS0wMVQwMDowMDowMFoiLCJpc3MiOiJodHRwczovL3Bhc3Nwb3J0LnR3aXRjaC50diIsImF1ZCI6IjJrOGl6MnE0eG1scDM2MmQyaWR5YzBoNnA0MmQxZSIsImV4cCI6IjIwMTctMDUtMDVUMjI6MzI6MjcuNzk1MzQxNjI1WiIsImlhdCI6MTQ5NDAyMjY0N30=";
        
        [TestMethod]
        public void VerifyRS256SignatureTestSuccess()
        {
            string sign = "6HkHCuwUufojZn3ogeyl8Bi0J8IyVjIzrTLLR-v-MpmP2-EYExjVT_aPjoIfuOmfK2cCDCsGSRL-p7rtFamuv3e4v--S_TkhekLioAdL-9Nm-ZnX8kdys9XYLEf8acFkQOmQ2W5DLfm68u3zAmpRMXsq_LPcsWVbPe5AGZK4Tt5mS5JIK1S8VCPTQLc7__rMI_3Hzpij09fILlbaicAKPqybLnqfMowyemcWmrecseNc_Jig_ZpGm7RqNkbxctBIBDouB_rGtH1R1CwlDs_PE5pSq4I67G6aoL0P4aUIOpFCXxLU45975ZdQDRRq3o2Lqce6cmRLOemO5JSCyTGZhQ==";

            RsaUtils hashUtils = new RsaUtils(modulus, exponent);

            bool result = hashUtils.VerifyRS256Signature(signedString, sign);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VerifyRS256SignatureTestFail()
        {
            string sign = "6HkHCuwUufojZn3ogeyl8Bi0JKI5VjIzr8LLR-v-MpmP2-EYExjVT_aPjoIfuOmfK2cCDCsGSRL-p7rtFamuv3e4v--S_TkhekLioAdL-9Nm-ZnX8kdys9XYLEf8acFkQOmQ2W5DLfm68u3zAmpRMXsq_LPcsWVbPe5AGZK4Tt5mS5JIK1S8VCPTQLc7__rMI_3Hzpij09fILlbaicAKPqybLnqfMowyemcWmrecseNc_Jig_ZpGm7RqNkbxctBIBDouB_rGtH1R1CwlDs_PE5pSq4I67G6aoL0P4aUIOpFCXxLU45975ZdQDRRq3o2Lqce6cmRLOemO5JSCyTGZhQ==";

            RsaUtils hashUtils = new RsaUtils(modulus, exponent);

            bool result = hashUtils.VerifyRS256Signature(signedString, sign);
            Assert.IsFalse(result);
        }
    }
}