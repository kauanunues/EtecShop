using EtecShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EtecShop.Data;
public class AppDbContext : IdentityDbContext
{
public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
{ }
public DbSet<Avaliacao> Avaliacoes { get; set; }
public DbSet<Categoria> Categorias { get; set; }
public DbSet<Produto> Produtos { get; set; }
protected override void OnModelCreating(ModelBuilder builder)
{
base.OnModelCreating(builder);
#region Populando os dados da Gestão de Usuários
List<IdentityRole> roles = new()
{
new IdentityRole()
{
Id = Guid.NewGuid().ToString(),
Name = "Administrador",
NormalizedName = "ADMINISTRADOR"
},
new IdentityRole()
{
Id = Guid.NewGuid().ToString(),
Name = "Funcionário",
NormalizedName = "FUNCIONÁRIO"
},
new IdentityRole()
{
Id = Guid.NewGuid().ToString(),
Name = "Cliente",
NormalizedName = "CLIENTE"
}
};
builder.Entity<IdentityRole>().HasData(roles);
IdentityUser user = new()
{
Id = Guid.NewGuid().ToString(),
Email = "admin@etecshop.com",
NormalizedEmail = "ADMIN@ETECSHOP.COM",
UserName = "Admin",
NormalizedUserName = "ADMIN",
LockoutEnabled = true,
EmailConfirmed = true,
};
PasswordHasher<IdentityUser> pass = new();
user.PasswordHash = pass.HashPassword(user, "@Etec123");
builder.Entity<IdentityUser>().HasData(user);
List<IdentityUserRole<string>> userRoles = new()
{
new IdentityUserRole<string>() {
UserId = user.Id,
RoleId = roles[0].Id
},
new IdentityUserRole<string>() {
UserId = user.Id,
RoleId = roles[1].Id
},
new IdentityUserRole<string>() {
UserId = user.Id,
RoleId = roles[2].Id
}};
builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
#endregion
}
}
#region Popular Categorias
List<Categoria> categorias = new(){
new Categoria() {
Id = 1,
Nome = "Livros e papelaria"
},
new Categoria() {
Id = 2,
Nome = "Games e PC Gamer"
},
new Categoria() {
Id = 3,
Nome = "Informática"
},
new Categoria() {
Id = 4,
Nome = "Smartphones"
},
new Categoria() {
Id = 5,
Nome = "Eletrodomesticos e Casa"
},
new Categoria() {
Id = 6,
Nome = "Beleza e Perfumaria"
},
new Categoria() {
Id = 7,
Nome = "Móveis e Decoração"

builder.Entity<Categoria>().HasData(categorias);
#endregion 
Id = 7,
Nome = "Smartphone Motorola Moto G04 4g 128gb Tela 6.6' 4gb Ram Câmera16mp Selfie 5mp - Grafite",
Descricao = "O novo Moto G04 foi projetado para impressionar com coresvibrantes, material superior e detalhes lindos. Beleza em alto nível! Feito com materialde padrão elevado, é fino e confortável ao toque.",
Preco = 699M,
Estoque = 40,
CategoriaId = 4,
Foto = "/img/produtos/7.png"
},
new Produto(){
Id = 8,
Nome = "Cervejeira Consul Mais 82 Litros Frost Free Czd12at Titanium -220v",
Descricao = "Com uma Cervejeira Consul Mais você não precisa mais sepreocupar se suas cervejas vão congelar ou se elas não vão ficar geladas na hora que opessoal chegar. Você pode mudar a configuração das prateleiras da sua Cervejeira.",
Preco = 2166.03M,
Estoque = 4,
CategoriaId = 5,
Foto = "/img/produtos/8.png"
   }
};
builder.Entity<Produto>().HasData(produtos);
#endregion
  }
 }
 AppDbContext context)
{
_logger = logger;
_context = context;
}
public IActionResult Index()
{
HomeVM homeVM = new(){
Categorias = _context.Categorias.ToList(),
Produtos = _context.Produtos.Include(p => p.Categoria).ToList()
};
return View(homeVM);
}
public IActionResult Produto(int id)
{
Produto produto = _context.Produtos
.Include(p => p.Categoria)
.FirstOrDefault(p => p.Id == id);
return View(produto);
}
public IActionResult Privacy()
{
return View();
} [
ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
return View(new ErrorViewModel { RequestId = Activity
.Current?.Id ?? HttpContext.TraceIdentifier });
}
}