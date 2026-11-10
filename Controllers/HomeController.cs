using CadastroFaculdade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Diagnostics;

namespace CadastroFaculdade.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar(Aluno al)
        {
            String cmdInsert = @"
                INSERT INTO aluno(nome,curso)
                values(@n, @c)";


           SqliteConnection conexao =  PegarConexao();
           SqliteCommand comando = new SqliteCommand(cmdInsert, conexao);
            comando.Parameters.AddWithValue("@n", al.Nome);
            comando.Parameters.AddWithValue("@c", al.Curso);
            comando.ExecuteNonQuery();

            TempData["mensagem"] = "Dados salvos com sucesso!";


            return RedirectToAction ("Index");
        }

        public SqliteConnection PegarConexao()
        {
            string caminho = "Data Source = faculdade.db";
            SqliteConnection conexao = new SqliteConnection(caminho);
            conexao.Open();
            string sql = @"create table IF NOT EXISTS aluno(
                              ra integer primary key autoincrement,
                              nome text not null, 
                              curso text not null
                            )
                          ";
            SqliteCommand command =new SqliteCommand(sql, conexao);
            command.ExecuteNonQuery();
            return conexao;
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
