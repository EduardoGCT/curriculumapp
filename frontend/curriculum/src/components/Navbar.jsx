import { Link } from 'react-router-dom';

export default function Navbar() {
  return (
    <nav style={{ 
      display: 'flex', 
      gap: '20px', 
      padding: '15px', 
      background: '#222', 
      marginBottom: '20px',
      borderRadius: '8px'
    }}>

      <Link to="/" style={{ color: 'white', textDecoration: 'none', fontWeight: 'bold' }}>Inicial</Link>
      <Link to="/sobre" style={{ color: 'white', textDecoration: 'none', fontWeight: 'bold' }}>Sobre Mim</Link>
      <Link to="/cadastro" style={{ color: 'white', textDecoration: 'none', fontWeight: 'bold' }}>Cadastrar</Link>
    </nav>
  );
}
