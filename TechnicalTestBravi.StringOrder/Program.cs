var exit = "S";
do
{
    Console.WriteLine("Informe a sequência de cochetes: ");
    var input = Console.ReadLine();
    var isValid = ValidateSequence(input);
    Console.WriteLine($"A sequência informada: {input} é {(isValid ? "válida" : "inválida")}");
    Console.WriteLine("Deseja preencher outra sequência? [S\\N]");
    exit = Console.ReadLine();
    if (exit.ToUpper() == "S")
        Console.Clear();

} while (exit.ToUpper() == "S");



static bool ValidateSequence(string input)
{
    List<int> ascIIList = new List<int>()
    {
        (int)'[',
        (int)']',
        (int)'{',
        (int)'}',
        (int)'(',
        (int)')',
    };

    foreach(char c in input)
    {
        if (!ascIIList.Contains(c))
            return false;
    }

    Stack<char> stack = new Stack<char>();

    foreach (char c in input)
    {
        var characterBracketVerification = VerifiedCharacter(c, stack, '[', ']');
        if (!characterBracketVerification)
            return false;

        var characterKeyVerification = VerifiedCharacter(c, stack, '{', '}');
        if (!characterKeyVerification)
            return false;

        var characterParathensisVerification = VerifiedCharacter(c, stack, '(', ')');
        if (!characterParathensisVerification)
            return false;

    }

    return stack.Count == 0;
}

static bool VerifiedCharacter(char input, Stack<char> stack, char characterOpen, char characterClose)
{
    if (input == characterOpen)
        stack.Push(input);
    else if (input == characterClose)
    {
        if (stack.Count == 0 || stack.Pop() != characterOpen)
            return false;
    }

    return true;
}