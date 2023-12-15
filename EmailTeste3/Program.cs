using SendGrid.Helpers.Mail;
using SendGrid;

class Program {
    static async Task Main() {
        await Execute();
    }

    static async Task Execute() {
        var apiKey = "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";
        var client = new SendGridClient(apiKey);

        // Solicitar ao usuário que insira os dados do formulário
        Console.Write("Nome: ");
        var name = Console.ReadLine();

        Console.Write("Email: ");
        var mail = Console.ReadLine();

        Console.Write("Telefone: ");
        var phone = Console.ReadLine();

        Console.WriteLine("Selecione o assunto:");
        Console.WriteLine("1. Dúvidas sobre Serviços");
        Console.WriteLine("2. Elogios");
        Console.WriteLine("3. Reclamações");
        Console.WriteLine("4. Outros");
        Console.Write("Opção (1-4): ");
        var subjectOption = int.Parse(Console.ReadLine());

        var subjectOptions = new Dictionary<int, string>
        {
            {1, "Dúvidas sobre Serviços"},
            {2, "Elogios"},
            {3, "Reclamações"},
            {4, "Outros"}
        };

        var subject = subjectOptions[subjectOption];

        Console.Write("Descrição: ");
        var message = Console.ReadLine();

        // Construir corpo do email com os dados inseridos pelo usuário
        var from = new EmailAddress("specretojasesbe@gmail.com", "Andre Lucas Farias");
        var to = new EmailAddress("andre.farias@aluno.unc.br", "Andre");
        var subjectText = $"Formulário de Contato - {subject}";
        var plainTextContent = $"Nome: {name}\nEmail: {mail}\nTelefone: {phone}\nAssunto: {subject}\nMensagem: {message}";
        var htmlContent = $"<strong>Nome:</strong> {name}<br><strong>Email:</strong> {mail}<br><strong>Telefone:</strong> {phone}<br><strong>Assunto:</strong> {subject}<br><strong>Mensagem:</strong> {message}";

        var msg = MailHelper.CreateSingleEmail(from, to, subjectText, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);

        Console.WriteLine(response.StatusCode);
        Console.WriteLine(await response.Body.ReadAsStringAsync());
    }
}
