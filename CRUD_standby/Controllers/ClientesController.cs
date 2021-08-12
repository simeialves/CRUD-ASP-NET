using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_standby.Models;

namespace CRUD_standby.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;
        string rsDuplicado;
        string cnpjDuplicado;
        
        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(string RazaoSocial = "", string Cnpj = "", char Status_cliente = '1')
        {
            var sql = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(RazaoSocial))
            {
                sql = sql.Where(c => c.Razao_social.Contains(RazaoSocial));
            }
            if (!string.IsNullOrEmpty(Cnpj))
            {
                sql = sql.Where(c => c.Cnpj.Contains(Cnpj));
            }
            if (Status_cliente == '1')
            {
                sql = sql.Where(c => c.Status_cliente == true);
            }
            else if (Status_cliente == '0')
                sql = sql.Where(c => c.Status_cliente == false);
            sql = sql.OrderBy(c => c.Razao_social);

            return View(sql.ToList());

        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Razao_social,Cnpj,Data_fundacao,Capital,Quarentena,Status_cliente,Classificacao")] Cliente cliente)
        {
            var sql = _context.Clientes.AsQueryable();
            bool validRS = false;
            bool validCNPJ = false;

            foreach (Cliente c in sql)
            {
                if (c.Cnpj == cliente.Cnpj)
                {
                    rsDuplicado = c.Razao_social;
                    validCNPJ = true;
                    break;
                }
            }

            foreach (Cliente c in sql)
            {
                if (c.Razao_social == cliente.Razao_social)
                {
                    cnpjDuplicado = c.Cnpj;
                    validRS = true;
                    break;
                }
            }

            if (!validRS)
            {
                if (!validCNPJ)
                {
                    if (ModelState.IsValid)
                    {
                        DateTime dateTime = DateTime.Today;

                        if (cliente.Capital <= 10000)
                            cliente.Classificacao = 'C';
                        else if (cliente.Capital > 10000 && cliente.Capital <= 1000000)
                            cliente.Classificacao = 'B';
                        else if (cliente.Capital > 1000000)
                            cliente.Classificacao = 'A';

                        if (cliente.Data_fundacao > dateTime.AddYears(-1))
                            cliente.Quarentena = true;

                        _context.Add(cliente);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                    ModelState.AddModelError("Cnpj", "Já existe um cadastro com esse mesmo CNPJ em nome de " + rsDuplicado + ".");
            }
            else
                ModelState.AddModelError("Razao_Social", "Já existe um cadastro com essa mesma Razão Social para o CNPJ " + cnpjDuplicado + ".");


            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Razao_social,Cnpj,Data_fundacao,Capital,Quarentena,Status_cliente,Classificacao")] Cliente cliente)
        {
            bool validRS = false;
            bool validCNPJ = false;

            if (id != cliente.Id)
            {
                return NotFound();
            }

            var sql = _context.Clientes.AsQueryable();

            //foreach (Cliente c in sql)
            //{
            //    if (c.Cnpj == cliente.Cnpj && c.Id != id)
            //    {
            //        rsDuplicado = c.Razao_social;
            //        validCNPJ = true;
            //        break;
            //    }
            //}

            //foreach (Cliente c in sql)
            //{
            //    if (c.Razao_social == cliente.Razao_social && c.Id != cliente.Id)
            //    {
            //        cnpjDuplicado = c.Cnpj;
            //        validRS = true;
            //        break;
            //    }
            //}

            if (!validRS)
            {
                if (!validCNPJ)
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            DateTime dateTime = DateTime.Today;

                            if (cliente.Capital <= 10000)
                                cliente.Classificacao = 'C';
                            else if (cliente.Capital > 10000 && cliente.Capital <= 1000000)
                                cliente.Classificacao = 'B';
                            else if (cliente.Capital > 1000000)
                                cliente.Classificacao = 'A';

                            if (cliente.Data_fundacao > dateTime.AddYears(-1))
                                cliente.Quarentena = true;

                            _context.Update(cliente);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ClienteExists(cliente.Id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                    ModelState.AddModelError("Cnpj", "Já existe um cadastro com esse mesmo CNPJ em nome de " + rsDuplicado + ".");
            }
            else
                ModelState.AddModelError("Razao_Social", "Já existe um cadastro com essa mesma Razão Social para o CNPJ " + cnpjDuplicado + ".");

            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
