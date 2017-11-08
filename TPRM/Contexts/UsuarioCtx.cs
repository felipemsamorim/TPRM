using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TPRM.DA;
using TPRM.Models;

namespace TPRM.Contexts
{
    public class UsuarioCtx
    {
        public Usuario getUsuario(int id) 
        {
            return new TPRMContext().Usuario.Where(x => x.id == id).SingleOrDefault();
        }
        public List<Usuario> getUsuarios()
        {
            return new TPRMContext().Usuario.ToList();
        }
        public Usuario createUsuario(string login, string senha,Empresa empresa)
        {
            Usuario user = new Usuario()
            {
                id = 0,
                login = login,
                senha = EncryptText(senha),
                empresa = empresa,
                perfil = null

            };
            using (TPRMContext ctx = new TPRMContext())
            {
                try
                {
                    user.empresa = ctx.Empresa.Where(x => x.id == user.empresa.id).SingleOrDefault();
                    ctx.Usuario.Add(user);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }

            return user;
        }
        public Usuario createUsuario(string login, string senha, Perfil perfil, Empresa empresa)
        {
            Usuario user = new Usuario()
            {
                id = 0,
                login = login,
                senha = EncryptText(senha),
                empresa = empresa,
                perfil = perfil

            };
            using (TPRMContext ctx = new TPRMContext())
            {
                try
                {
                    user.perfil = ctx.Perfil.Where(x => x.id == user.perfil.id).SingleOrDefault();
                    user.empresa = ctx.Empresa.Where(x => x.id == user.empresa.id).SingleOrDefault();
                    ctx.Usuario.Add(user);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }

            return user;
        }
        public void updateUsuario(Usuario user)
        {
            using (var ctx = new TPRMContext())
            {
                try
                {
                    user.empresa = ctx.Empresa.Where(x => x.id == user.empresa.id).SingleOrDefault();
                    user.perfil = ctx.Perfil.Where(x => x.id == user.perfil.id).SingleOrDefault();
                    
                    ctx.Usuario.Where(x => x.id == user.id).SingleOrDefault().login = user.login;
                    ctx.Usuario.Where(x => x.id == user.id).SingleOrDefault().perfil = user.perfil;
                    ctx.Usuario.Where(x => x.id == user.id).SingleOrDefault().senha = user.senha;
                    ctx.Usuario.Where(x => x.id == user.id).SingleOrDefault().empresa = user.empresa;
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }
        }
        public void deleteUsuario(int id)
        {
            using (var ctx = new TPRMContext())
            {
                try
                {
                    ctx.Usuario.Remove(ctx.Usuario.Where(x => x.id == id).SingleOrDefault());
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }
        }
        public Usuario login(string login, string senha)
        {
            Usuario user = new Usuario();
            TPRMContext ctx = new TPRMContext();
            senha = EncryptText(senha);
            user = ctx.Usuario.Where(x => x.login == login && x.senha ==  senha).SingleOrDefault();
            return user;
        }
        public Perfil getPerfil(int id)
        {
            TPRMContext ctx = new TPRMContext();
            return ctx.Perfil.Where(p => p.id == id).SingleOrDefault();
        }
        public void createPerfil(Perfil p)
        {
            using (var ctx = new TPRMContext())
            {
                try
                {
                    ctx.Perfil.Add(p);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }
        }
        public void updatePerfil(Perfil p)
        {
            using (var ctx = new TPRMContext())
            {
                try
                {
                    ctx.Perfil.Where(x => x.id == p.id).SingleOrDefault().descricao = p.descricao;
                    ctx.Perfil.Where(x => x.id == p.id).SingleOrDefault().analista = p.analista;
                    ctx.Perfil.Where(x => x.id == p.id).SingleOrDefault().adm = p.adm;
                    ctx.SaveChanges();

                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }
        }
        public List<Perfil> getPerfilAll()
        {
            TPRMContext ctx = new TPRMContext();
            return ctx.Perfil.ToList();
        }

        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public string EncryptText(string input, string password = "TPRM2001120")
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        public string DecryptText(string input, string password = "TPRM2001120")
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }
    }
}