namespace Backend.JWTToken
{
    public struct TokenHead
    {
        public string type;
        public string alg;
    }
    public struct TokenBody
    {
        public int sub;
        public long sta;
    }
}
