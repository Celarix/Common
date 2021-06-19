namespace ChrisAkridge.Common.Options
{
    public static class Option
    {
        /*
         * Lifted wholesale from https://github.com/nlkl/Optional/
         * Optional is licensed with the MIT License, permitting
         * distribution and modification.
         * 
         * Why am I copying the project instead of including it?
         * It was last pushed to in February 2018. Plus, it seems
         * like fun to rewrite it.
         */
        
        public static Option<T> Some<T>(T value) => new Option<T>(value, true);

        public static Option<T, TException> Some<T, TException>(T value) =>
           new Option<T, TException>(value, default, true);
        
        public static Option<T> None<T>() => new Option<T>(default, false);
        
        public static Option<T, TException> None<T, TException>(TException exception) =>
            new Option<T, TException>(default, exception, false);
        
        
    }
}
