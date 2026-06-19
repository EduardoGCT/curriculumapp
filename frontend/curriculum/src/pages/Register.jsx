import { useState } from 'react';
import Navbar from '/src/components/Navbar';

export default function Register() {
  const [name, setName] = useState('');
  const [fullName, setFullName] = useState('');
  const [email, setEmail] = useState('');
  const [phone, setPhone] = useState('');
  const [summary, setSummary] = useState('');
  const [institution, setInstitution] = useState('');
  const [degree, setDegree] = useState('');
  const [eduDescription, setEduDescription] = useState('');
  const [experiences, setExperiences] = useState([]);
  const [company, setCompany] = useState('');
  const [role, setRole] = useState('');
  const [expDescription, setExpDescription] = useState('');
  const [skillName, setSkillName] = useState('');
  const [skillLevel, setSkillLevel] = useState('');
  const [carregando, setCarregando] = useState(false);
  const [sucesso, setSucesso] = useState(false);
  const [erro, setErro] = useState(null);

  const handleAddExperience = () => {
    if (!company.trim() || !role.trim()) {
      alert('Empresa e Cargo são obrigatórios.');
      return;
    }
    const novaExp = { company, role, start: null, end: null, description: expDescription || null };
    setExperiences([...experiences, novaExp]);
    setCompany('');
    setRole('');
    setExpDescription('');
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setCarregando(true);
    setSucesso(false);
    setErro(null);

    let listaFinalExperiencias = [...experiences];
    if (company.trim() && role.trim()) {
      listaFinalExperiencias.push({ company, role, start: null, end: null, description: expDescription || null });
    }

    const novoCurriculo = {
      name,
      personalInfo: { fullName, email: email || null, phone: phone || null, summary: summary || null },
      educations: institution && degree ? [{ institution, degree, start: null, end: null, description: eduDescription || null }] : [],
      experiences: listaFinalExperiencias,
      skills: skillName ? [{ name: skillName, level: skillLevel || null }] : []
    };

    fetch('http://127.0.0.1:5195/curriculum', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(novoCurriculo)
    })
    .then((resposta) => {
      if (!resposta.ok) throw new Error('Erro ao salvar no servidor');
      setSucesso(true);
      setName(''); setFullName(''); setEmail(''); setPhone(''); setSummary('');
      setInstitution(''); setDegree(''); setEduDescription(''); setExperiences([]);
      setCompany(''); setRole(''); setExpDescription(''); setSkillName(''); setSkillLevel('');
    })
    .catch((err) => setErro(err.message))
    .finally(() => setCarregando(false));
  };

  return (
    <div style={{ padding: '40px 20px', maxWidth: '650px', margin: '0 auto', fontFamily: 'sans-serif', backgroundColor: '#13151a', color: '#e3e6ed', minHeight: '100vh' }}>
      <Navbar />
      <h1>Cadastrar Currículo</h1>
      {sucesso && <p style={{ color: '#22c55e' }}>Salvo com sucesso!</p>}
      {erro && <p style={{ color: '#ef4444' }}>Erro: {erro}</p>}

      <form onSubmit={handleSubmit} style={{ backgroundColor: '#1c1f26', padding: '30px', borderRadius: '12px', border: '1px solid #2d3139' }}>
        <div style={{ marginBottom: '20px' }}>
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Nome de Busca:</label>
          <input type="text" value={name} onChange={(e) => setName(e.target.value)} required style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box' }} />
        </div>

        <div style={{ marginBottom: '20px' }}>
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Nome Completo:</label>
          <input type="text" value={fullName} onChange={(e) => setFullName(e.target.value)} required style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box' }} />
        </div>

        <div style={{ marginBottom: '20px' }}>
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Email:</label>
          <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box' }} />
        </div>

        <div style={{ marginBottom: '20px' }}>
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Telefone:</label>
          <input type="text" value={phone} onChange={(e) => setPhone(e.target.value)} style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box' }} />
        </div>

        <div style={{ marginBottom: '20px' }}>
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Resumo Profissional:</label>
          <textarea value={summary} onChange={(e) => setSummary(e.target.value)} style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box', height: '80px' }} />
        </div>

        <div style={{ marginBottom: '20px', borderTop: '1px solid #2d3139', paddingTop: '15px' }}>
          <h3 style={{ color: '#3b82f6', margin: '0 0 15px 0' }}>Educação (Opcional)</h3>
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Instituição:</label>
          <input type="text" value={institution} onChange={(e) => setInstitution(e.target.value)} style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box', marginBottom: '15px' }} />
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Curso / Grau:</label>
          <input type="text" value={degree} onChange={(e) => setDegree(e.target.value)} style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box', marginBottom: '15px' }} />
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Descrição da Formação:</label>
          <textarea value={eduDescription} onChange={(e) => setEduDescription(e.target.value)} style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box', height: '80px' }} />
        </div>

        <div style={{ marginBottom: '20px', borderTop: '1px solid #2d3139', paddingTop: '15px' }}>
          <h3 style={{ color: '#3b82f6', margin: '0 0 15px 0' }}>Experiência Profissional</h3>
          {experiences.map((exp, idx) => (
            <div key={idx} style={{ backgroundColor: '#13151a', padding: '10px', borderRadius: '6px', marginBottom: '10px', border: '1px solid #2d3139' }}>
              <strong>{exp.role}</strong> em {exp.company}
            </div>
          ))}
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Empresa:</label>
          <input type="text" value={company} onChange={(e) => setCompany(e.target.value)} style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box', marginBottom: '15px' }} />
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Cargo:</label>
          <input type="text" value={role} onChange={(e) => setRole(e.target.value)} style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box', marginBottom: '15px' }} />
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Descrição:</label>
          <textarea value={expDescription} onChange={(e) => setExpDescription(e.target.value)} style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box', height: '80px', marginBottom: '15px' }} />
          <button type="button" onClick={handleAddExperience} style={{ padding: '8px 14px', backgroundColor: '#10b981', color: '#fff', border: 'none', borderRadius: '6px', cursor: 'pointer' }}>+ Adicionar à Lista</button>
        </div>

        <div style={{ marginBottom: '25px', borderTop: '1px solid #2d3139', paddingTop: '15px' }}>
          <h3 style={{ color: '#3b82f6', margin: '0 0 15px 0' }}>Habilidade (Opcional)</h3>
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Nome:</label>
          <input type="text" value={skillName} onChange={(e) => setSkillName(e.target.value)} style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box', marginBottom: '15px' }} />
          <label style={{ display: 'block', marginBottom: '6px', color: '#9fa6b2' }}>Nível:</label>
          <input type="text" value={skillLevel} onChange={(e) => setSkillLevel(e.target.value)} style={{ width: '100%', padding: '10px', backgroundColor: '#13151a', color: '#fff', border: '1px solid #3f444e', borderRadius: '6px', boxSizing: 'border-box' }} />
        </div>

        <button type="submit" disabled={carregando} style={{ width: '100%', padding: '12px', fontSize: '16px', fontWeight: 'bold', color: '#ffffff', backgroundColor: carregando ? '#4b5563' : '#3b82f6', border: 'none', borderRadius: '6px', cursor: carregando ? 'not-allowed' : 'pointer' }}>
          {carregando ? 'Salvando...' : 'Salvar Currículo'}
        </button>
      </form>
    </div>
  );
}
