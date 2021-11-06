namespace HuntTheWumpus
{
    class Cell
    {
        private CellContent _content;

        public Cell (CellContent _content)
        {
            SetContent(_content);
        }

        public CellContent GetContent()
        {
            return _content;
        }

        public void SetContent(CellContent value)
        {
            _content = value;
        }
    }
}
