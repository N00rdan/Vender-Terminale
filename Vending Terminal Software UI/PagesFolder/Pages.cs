namespace Vending_Terminal_Software_UI.PagesFolder
{
    static class Pages
    {
        private static SelectVM selectVM = new SelectVM();
        private static VM vM = new VM();

        public static SelectVM SelectVM
        {
            get { return selectVM; }
        }

        public static VM VM
        {
            get { return vM; }
        }
    }
}
