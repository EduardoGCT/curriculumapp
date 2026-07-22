import { useState, useEffect } from 'react';
import Navbar from '/src/components/Navbar';

export default function MeuCurriculo() {
  const [curriculosLista, setCurriculosLista] = useState([]);
  const [curriculoSelecionado, setCurriculoSelecionado] = useState(null);
  const [carregando, setCarregando] = useState(false);
  const [erro, setErro] = useState(null);
  const [buscaNome, setBuscaNome] = useState('');

  useEffect(() => {
    setCarregando(true);
    fetch('http://127.0.0.1:5195/curriculum')
      .then((resposta) => {
        if (!resposta.ok) throw new Error('Erro ao buscar dados');
        return resposta.json();
      })
      .then((dados) => {
        setCurriculosLista(dados);
        setCarregando(false);
      })
      .catch((err) => {
        setErro(err.message);
        setCarregando(false);
      });
  }, []);

  useEffect(() => {
    if (!buscaNome.trim()) {
      setCurriculoSelecionado(null);
      return;
    }

    const encontrado = curriculosLista.find(c =>
      c.name && c.name.toLowerCase().includes(buscaNome.toLowerCase())
    );

    setCurriculoSelecionado(encontrado || null);
  }, [buscaNome, curriculosLista]);

  // Objetos de Estilização (Design System)
  const styles = {
    container: {
      padding: '40px 20px',
      maxWidth: '750px',
      margin: '0 auto',
      fontFamily: '"Segoe UI", Roboto, Helvetica, Arial, sans-serif',
      backgroundColor: '#13151a',
      color: '#e3e6ed',
      minHeight: '100vh',
    },
    searchGroup: {
      marginBottom: '35px',
      backgroundColor: '#1c1f26',
      padding: '20px',
      borderRadius: '12px',
      border: '1px solid #2d3139',
      boxShadow: '0 4px 12px rgba(0,0,0,0.2)',
    },
    label: {
      display: 'block',
      marginBottom: '10px',
      fontWeight: '600',
      fontSize: '15px',
      color: '#9fa6b2',
    },
    input: {
      width: '100%',
      padding: '12px 16px',
      fontSize: '16px',
      borderRadius: '8px',
      border: '1px solid #3f444e',
      backgroundColor: '#13151a',
      color: '#ffffff',
      outline: 'none',
      transition: 'border-color 0.2s',
      boxSizing: 'border-box',
    },
    card: {
      backgroundColor: '#1c1f26',
      padding: '30px',
      borderRadius: '16px',
      border: '1px solid #2d3139',
      boxShadow: '0 8px 24px rgba(0,0,0,0.3)',
    },
    mainTitle: {
      fontSize: '32px',
      fontWeight: '700',
      color: '#ffffff',
      margin: '0 0 25px 0',
      paddingBottom: '15px',
      borderBottom: '2px solid #3b82f6',
    },
    section: {
      marginBottom: '30px',
    },
    sectionTitle: {
      fontSize: '20px',
      fontWeight: '600',
      color: '#3b82f6',
      marginBottom: '15px',
      borderBottom: '1px solid #2d3139',
      paddingBottom: '5px',
    },
    infoBlock: {
      backgroundColor: '#13151a',
      padding: '20px',
      borderRadius: '8px',
      border: '1px solid #2d3139',
    },
    text: {
      margin: '8px 0',
      fontSize: '15px',
      lineHeight: '1.6',
      color: '#cbd5e1',
    },
    itemBlock: {
      borderLeft: '3px solid #3b82f6',
      paddingLeft: '15px',
      marginBottom: '20px',
    },
    itemTitle: {
      margin: '0 0 5px 0',
      fontSize: '18px',
      fontWeight: '600',
      color: '#ffffff',
    },
    list: {
      paddingLeft: '20px',
      margin: '0',
    },
    listItem: {
      marginBottom: '10px',
      fontSize: '15px',
      color: '#cbd5e1',
    },
    badge: {
      color: '#3b82f6',
      backgroundColor: 'rgba(59, 130, 246, 0.1)',
      padding: '2px 8px',
      borderRadius: '4px',
      fontSize: '13px',
      fontWeight: '600',
      marginLeft: '10px',
    },
    statusText: {
      color: '#9fa6b2',
      textAlign: 'center',
      marginTop: '20px',
      fontSize: '15px',
    }
  };

  return (
    <div style={styles.container}>
      <Navbar />

      <div style={styles.searchGroup}>
        <label htmlFor="busca" style={styles.label}>
          Buscar Currículo por Nome:
        </label>
        <input
          id="busca"
          type="text"
          placeholder="Ex: Eduardo..."
          value={buscaNome}
          onChange={(e) => setBuscaNome(e.target.value)}
          style={styles.input}
        />
      </div>

      {carregando && <p style={styles.statusText}>Carregando...</p>}
      {erro && <p style={{ ...styles.statusText, color: '#ef4444' }}>Erro: {erro}</p>}
      {!buscaNome.trim() && <p style={styles.statusText}>Digite um nome para buscar o currículo.</p>}

      {!carregando && curriculoSelecionado && (
        <div style={styles.card}>
          <h1 style={styles.mainTitle}>{curriculoSelecionado.name}</h1>

          {curriculoSelecionado.personalInfo && (
            <section style={styles.section}>
              <h3 style={styles.sectionTitle}>Informações Pessoais</h3>
              <div style={styles.infoBlock}>
                <p style={styles.text}><strong>Nome Completo:</strong> {curriculoSelecionado.personalInfo.fullName}</p>
                <p style={styles.text}><strong>Email:</strong> {curriculoSelecionado.personalInfo.email}</p>
                <p style={styles.text}><strong>Telefone:</strong> {curriculoSelecionado.personalInfo.phone}</p>
                <p style={styles.text}><strong>Resumo:</strong> {curriculoSelecionado.personalInfo.summary}</p>
              </div>
            </section>
          )}

          <section style={styles.section}>
            <h3 style={styles.sectionTitle}>Experiências Profissionais</h3>
            {curriculoSelecionado.experiences && curriculoSelecionado.experiences.length > 0 ? (
              curriculoSelecionado.experiences.map((exp) => (
                <div key={exp.id} style={styles.itemBlock}>
                  <h4 style={styles.itemTitle}>{exp.role} — {exp.company}</h4>
                  <p style={styles.text}>{exp.description}</p>
                </div>
              ))
            ) : <p style={styles.text}>Nenhuma experiência cadastrada.</p>}
          </section>

          <section style={styles.section}>
            <h3 style={styles.sectionTitle}>Educação</h3>
            {curriculoSelecionado.educations && curriculoSelecionado.educations.length > 0 ? (
              curriculoSelecionado.educations.map((edu) => (
                <div key={edu.id} style={styles.itemBlock}>
                  <h4 style={styles.itemTitle}>{edu.degree} em {edu.institution}</h4>
                  <p style={styles.text}>{edu.description}</p>
                </div>
              ))
            ) : <p style={styles.text}>Nenhuma formação acadêmica cadastrada.</p>}
          </section>

          <section style={styles.section}>
            <h3 style={styles.sectionTitle}>Habilidades</h3>
            {curriculoSelecionado.skills && curriculoSelecionado.skills.length > 0 ? (
              <ul style={styles.list}>
                {curriculoSelecionado.skills.map((skill) => (
                  <li key={skill.id} style={styles.listItem}>
                    <strong>{skill.name}</strong> 
                    {skill.level && <span style={styles.badge}>{skill.level}</span>}
                  </li>
                ))}
              </ul>
            ) : <p style={styles.text}>Nenhuma habilidade cadastrada.</p>}
          </section>
        </div>
      )}

      {!carregando && buscaNome.trim() && !curriculoSelecionado && (
        <p style={{ ...styles.statusText, color: '#f59e0b' }}>Nenhum currículo encontrado com o nome "{buscaNome}".</p>
      )}
    </div>
  );
}
