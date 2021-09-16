namespace HuntTheWumpus
{
    class Cell
    {
        public CellContent Content { get; set; }

        public Cell (CellContent content)
        {
            Content = content;
        }
    }
}
