function O() {
}
function Je(t, e) {
  for (const n in e)
    t[n] = e[n];
  return t;
}
function Pe(t) {
  return t();
}
function ke() {
  return /* @__PURE__ */ Object.create(null);
}
function X(t) {
  t.forEach(Pe);
}
function ge(t) {
  return typeof t == "function";
}
function Y(t, e) {
  return t != t ? e == e : t !== e || t && typeof t == "object" || typeof t == "function";
}
function Ke(t) {
  return Object.keys(t).length === 0;
}
function Qe(t, ...e) {
  if (t == null)
    return O;
  const n = t.subscribe(...e);
  return n.unsubscribe ? () => n.unsubscribe() : n;
}
function a(t, e) {
  t.appendChild(e);
}
function E(t, e, n) {
  t.insertBefore(e, n || null);
}
function k(t) {
  t.parentNode && t.parentNode.removeChild(t);
}
function be(t, e) {
  for (let n = 0; n < t.length; n += 1)
    t[n] && t[n].d(e);
}
function p(t) {
  return document.createElement(t);
}
function G(t) {
  return document.createTextNode(t);
}
function j() {
  return G(" ");
}
function ae() {
  return G("");
}
function Q(t, e, n, s) {
  return t.addEventListener(e, n, s), () => t.removeEventListener(e, n, s);
}
function b(t, e, n) {
  n == null ? t.removeAttribute(e) : t.getAttribute(e) !== n && t.setAttribute(e, n);
}
function Ie(t) {
  return t === "" ? null : +t;
}
function Ve(t) {
  return Array.from(t.childNodes);
}
function le(t, e) {
  e = "" + e, t.wholeText !== e && (t.data = e);
}
function oe(t, e) {
  t.value = e ?? "";
}
function qe(t, e) {
  for (let n = 0; n < t.options.length; n += 1) {
    const s = t.options[n];
    if (s.__value === e) {
      s.selected = !0;
      return;
    }
  }
  t.selectedIndex = -1;
}
function Ze(t) {
  const e = t.querySelector(":checked") || t.options[0];
  return e && e.__value;
}
function et(t, e, { bubbles: n = !1, cancelable: s = !1 } = {}) {
  const r = document.createEvent("CustomEvent");
  return r.initCustomEvent(t, n, s, e), r;
}
function he(t, e) {
  return new t(e);
}
let ue;
function ce(t) {
  ue = t;
}
function ye() {
  if (!ue)
    throw new Error("Function called outside component initialization");
  return ue;
}
function tt(t) {
  ye().$$.after_update.push(t);
}
function nt(t) {
  ye().$$.on_destroy.push(t);
}
function Me() {
  const t = ye();
  return (e, n, { cancelable: s = !1 } = {}) => {
    const r = t.$$.callbacks[e];
    if (r) {
      const c = et(e, n, { cancelable: s });
      return r.slice().forEach((i) => {
        i.call(t, c);
      }), !c.defaultPrevented;
    }
    return !0;
  };
}
function Ee(t, e) {
  const n = t.$$.callbacks[e.type];
  n && n.slice().forEach((s) => s.call(this, e));
}
const ie = [], Ne = [], fe = [], Se = [], ze = Promise.resolve();
let me = !1;
function He() {
  me || (me = !0, ze.then(Fe));
}
function st() {
  return He(), ze;
}
function pe(t) {
  fe.push(t);
}
const _e = /* @__PURE__ */ new Set();
let se = 0;
function Fe() {
  if (se !== 0)
    return;
  const t = ue;
  do {
    try {
      for (; se < ie.length; ) {
        const e = ie[se];
        se++, ce(e), rt(e.$$);
      }
    } catch (e) {
      throw ie.length = 0, se = 0, e;
    }
    for (ce(null), ie.length = 0, se = 0; Ne.length; )
      Ne.pop()();
    for (let e = 0; e < fe.length; e += 1) {
      const n = fe[e];
      _e.has(n) || (_e.add(n), n());
    }
    fe.length = 0;
  } while (ie.length);
  for (; Se.length; )
    Se.pop()();
  me = !1, _e.clear(), ce(t);
}
function rt(t) {
  if (t.fragment !== null) {
    t.update(), X(t.before_update);
    const e = t.dirty;
    t.dirty = [-1], t.fragment && t.fragment.p(t.ctx, e), t.after_update.forEach(pe);
  }
}
const de = /* @__PURE__ */ new Set();
let V;
function ve() {
  V = {
    r: 0,
    c: [],
    p: V
    // parent group
  };
}
function we() {
  V.r || X(V.c), V = V.p;
}
function I(t, e) {
  t && t.i && (de.delete(t), t.i(e));
}
function H(t, e, n, s) {
  if (t && t.o) {
    if (de.has(t))
      return;
    de.add(t), V.c.push(() => {
      de.delete(t), s && (n && t.d(1), s());
    }), t.o(e);
  } else
    s && s();
}
function Ge(t, e) {
  const n = {}, s = {}, r = { $$scope: 1 };
  let c = t.length;
  for (; c--; ) {
    const i = t[c], l = e[c];
    if (l) {
      for (const o in i)
        o in l || (s[o] = 1);
      for (const o in l)
        r[o] || (n[o] = l[o], r[o] = 1);
      t[c] = l;
    } else
      for (const o in i)
        r[o] = 1;
  }
  for (const i in s)
    i in n || (n[i] = void 0);
  return n;
}
function Be(t) {
  return typeof t == "object" && t !== null ? t : {};
}
function Z(t) {
  t && t.c();
}
function B(t, e, n, s) {
  const { fragment: r, after_update: c } = t.$$;
  r && r.m(e, n), s || pe(() => {
    const i = t.$$.on_mount.map(Pe).filter(ge);
    t.$$.on_destroy ? t.$$.on_destroy.push(...i) : X(i), t.$$.on_mount = [];
  }), c.forEach(pe);
}
function U(t, e) {
  const n = t.$$;
  n.fragment !== null && (X(n.on_destroy), n.fragment && n.fragment.d(e), n.on_destroy = n.fragment = null, n.ctx = []);
}
function it(t, e) {
  t.$$.dirty[0] === -1 && (ie.push(t), He(), t.$$.dirty.fill(0)), t.$$.dirty[e / 31 | 0] |= 1 << e % 31;
}
function ee(t, e, n, s, r, c, i, l = [-1]) {
  const o = ue;
  ce(t);
  const u = t.$$ = {
    fragment: null,
    ctx: [],
    // state
    props: c,
    update: O,
    not_equal: r,
    bound: ke(),
    // lifecycle
    on_mount: [],
    on_destroy: [],
    on_disconnect: [],
    before_update: [],
    after_update: [],
    context: new Map(e.context || (o ? o.$$.context : [])),
    // everything else
    callbacks: ke(),
    dirty: l,
    skip_bound: !1,
    root: e.target || o.$$.root
  };
  i && i(u.root);
  let f = !1;
  if (u.ctx = n ? n(t, e.props || {}, (g, y, ...v) => {
    const N = v.length ? v[0] : y;
    return u.ctx && r(u.ctx[g], u.ctx[g] = N) && (!u.skip_bound && u.bound[g] && u.bound[g](N), f && it(t, g)), y;
  }) : [], u.update(), f = !0, X(u.before_update), u.fragment = s ? s(u.ctx) : !1, e.target) {
    if (e.hydrate) {
      const g = Ve(e.target);
      u.fragment && u.fragment.l(g), g.forEach(k);
    } else
      u.fragment && u.fragment.c();
    e.intro && I(t.$$.fragment), B(t, e.target, e.anchor, e.customElement), Fe();
  }
  ce(o);
}
class te {
  $destroy() {
    U(this, 1), this.$destroy = O;
  }
  $on(e, n) {
    if (!ge(n))
      return O;
    const s = this.$$.callbacks[e] || (this.$$.callbacks[e] = []);
    return s.push(n), () => {
      const r = s.indexOf(n);
      r !== -1 && s.splice(r, 1);
    };
  }
  $set(e) {
    this.$$set && !Ke(e) && (this.$$.skip_bound = !0, this.$$set(e), this.$$.skip_bound = !1);
  }
}
const re = [];
function Ue(t, e) {
  return {
    subscribe: Xe(t, e).subscribe
  };
}
function Xe(t, e = O) {
  let n;
  const s = /* @__PURE__ */ new Set();
  function r(l) {
    if (Y(t, l) && (t = l, n)) {
      const o = !re.length;
      for (const u of s)
        u[1](), re.push(u, t);
      if (o) {
        for (let u = 0; u < re.length; u += 2)
          re[u][0](re[u + 1]);
        re.length = 0;
      }
    }
  }
  function c(l) {
    r(l(t));
  }
  function i(l, o = O) {
    const u = [l, o];
    return s.add(u), s.size === 1 && (n = e(r) || O), l(t), () => {
      s.delete(u), s.size === 0 && (n(), n = null);
    };
  }
  return { set: r, update: c, subscribe: i };
}
function Ye(t, e, n) {
  const s = !Array.isArray(t), r = s ? [t] : t, c = e.length < 2;
  return Ue(n, (i) => {
    let l = !1;
    const o = [];
    let u = 0, f = O;
    const g = () => {
      if (u)
        return;
      f();
      const v = e(s ? o[0] : o, i);
      c ? i(v) : f = ge(v) ? v : O;
    }, y = r.map((v, N) => Qe(v, (q) => {
      o[N] = q, u &= ~(1 << N), l && g();
    }, () => {
      u |= 1 << N;
    }));
    return l = !0, g(), function() {
      X(y), f();
    };
  });
}
function ot(t, e) {
  if (t instanceof RegExp)
    return { keys: !1, pattern: t };
  var n, s, r, c, i = [], l = "", o = t.split("/");
  for (o[0] || o.shift(); r = o.shift(); )
    n = r[0], n === "*" ? (i.push("wild"), l += "/(.*)") : n === ":" ? (s = r.indexOf("?", 1), c = r.indexOf(".", 1), i.push(r.substring(1, ~s ? s : ~c ? c : r.length)), l += ~s && !~c ? "(?:/([^/]+?))?" : "/([^/]+?)", ~c && (l += (~s ? "?" : "") + "\\" + r.substring(c))) : l += "/" + r;
  return {
    keys: i,
    pattern: new RegExp("^" + l + (e ? "(?=$|/)" : "/?$"), "i")
  };
}
function lt(t) {
  let e, n, s;
  const r = [
    /*props*/
    t[2]
  ];
  var c = (
    /*component*/
    t[0]
  );
  function i(l) {
    let o = {};
    for (let u = 0; u < r.length; u += 1)
      o = Je(o, r[u]);
    return { props: o };
  }
  return c && (e = he(c, i()), e.$on(
    "routeEvent",
    /*routeEvent_handler_1*/
    t[7]
  )), {
    c() {
      e && Z(e.$$.fragment), n = ae();
    },
    m(l, o) {
      e && B(e, l, o), E(l, n, o), s = !0;
    },
    p(l, o) {
      const u = o & /*props*/
      4 ? Ge(r, [Be(
        /*props*/
        l[2]
      )]) : {};
      if (c !== (c = /*component*/
      l[0])) {
        if (e) {
          ve();
          const f = e;
          H(f.$$.fragment, 1, 0, () => {
            U(f, 1);
          }), we();
        }
        c ? (e = he(c, i()), e.$on(
          "routeEvent",
          /*routeEvent_handler_1*/
          l[7]
        ), Z(e.$$.fragment), I(e.$$.fragment, 1), B(e, n.parentNode, n)) : e = null;
      } else
        c && e.$set(u);
    },
    i(l) {
      s || (e && I(e.$$.fragment, l), s = !0);
    },
    o(l) {
      e && H(e.$$.fragment, l), s = !1;
    },
    d(l) {
      l && k(n), e && U(e, l);
    }
  };
}
function ct(t) {
  let e, n, s;
  const r = [
    { params: (
      /*componentParams*/
      t[1]
    ) },
    /*props*/
    t[2]
  ];
  var c = (
    /*component*/
    t[0]
  );
  function i(l) {
    let o = {};
    for (let u = 0; u < r.length; u += 1)
      o = Je(o, r[u]);
    return { props: o };
  }
  return c && (e = he(c, i()), e.$on(
    "routeEvent",
    /*routeEvent_handler*/
    t[6]
  )), {
    c() {
      e && Z(e.$$.fragment), n = ae();
    },
    m(l, o) {
      e && B(e, l, o), E(l, n, o), s = !0;
    },
    p(l, o) {
      const u = o & /*componentParams, props*/
      6 ? Ge(r, [
        o & /*componentParams*/
        2 && { params: (
          /*componentParams*/
          l[1]
        ) },
        o & /*props*/
        4 && Be(
          /*props*/
          l[2]
        )
      ]) : {};
      if (c !== (c = /*component*/
      l[0])) {
        if (e) {
          ve();
          const f = e;
          H(f.$$.fragment, 1, 0, () => {
            U(f, 1);
          }), we();
        }
        c ? (e = he(c, i()), e.$on(
          "routeEvent",
          /*routeEvent_handler*/
          l[6]
        ), Z(e.$$.fragment), I(e.$$.fragment, 1), B(e, n.parentNode, n)) : e = null;
      } else
        c && e.$set(u);
    },
    i(l) {
      s || (e && I(e.$$.fragment, l), s = !0);
    },
    o(l) {
      e && H(e.$$.fragment, l), s = !1;
    },
    d(l) {
      l && k(n), e && U(e, l);
    }
  };
}
function ut(t) {
  let e, n, s, r;
  const c = [ct, lt], i = [];
  function l(o, u) {
    return (
      /*componentParams*/
      o[1] ? 0 : 1
    );
  }
  return e = l(t), n = i[e] = c[e](t), {
    c() {
      n.c(), s = ae();
    },
    m(o, u) {
      i[e].m(o, u), E(o, s, u), r = !0;
    },
    p(o, [u]) {
      let f = e;
      e = l(o), e === f ? i[e].p(o, u) : (ve(), H(i[f], 1, 1, () => {
        i[f] = null;
      }), we(), n = i[e], n ? n.p(o, u) : (n = i[e] = c[e](o), n.c()), I(n, 1), n.m(s.parentNode, s));
    },
    i(o) {
      r || (I(n), r = !0);
    },
    o(o) {
      H(n), r = !1;
    },
    d(o) {
      i[e].d(o), o && k(s);
    }
  };
}
function je() {
  const t = window.location.href.indexOf("#/");
  let e = t > -1 ? window.location.href.substr(t + 1) : "/";
  const n = e.indexOf("?");
  let s = "";
  return n > -1 && (s = e.substr(n + 1), e = e.substr(0, n)), { location: e, querystring: s };
}
const $e = Ue(
  null,
  // eslint-disable-next-line prefer-arrow-callback
  function(e) {
    e(je());
    const n = () => {
      e(je());
    };
    return window.addEventListener("hashchange", n, !1), function() {
      window.removeEventListener("hashchange", n, !1);
    };
  }
);
Ye($e, (t) => t.location);
Ye($e, (t) => t.querystring);
const Oe = Xe(void 0);
function at(t) {
  t ? window.scrollTo(t.__svelte_spa_router_scrollX, t.__svelte_spa_router_scrollY) : window.scrollTo(0, 0);
}
function ft(t, e, n) {
  let { routes: s = {} } = e, { prefix: r = "" } = e, { restoreScrollState: c = !1 } = e;
  class i {
    /**
    * Initializes the object and creates a regular expression from the path, using regexparam.
    *
    * @param {string} path - Path to the route (must start with '/' or '*')
    * @param {SvelteComponent|WrappedComponent} component - Svelte component for the route, optionally wrapped
    */
    constructor(m, h) {
      if (!h || typeof h != "function" && (typeof h != "object" || h._sveltesparouter !== !0))
        throw Error("Invalid component object");
      if (!m || typeof m == "string" && (m.length < 1 || m.charAt(0) != "/" && m.charAt(0) != "*") || typeof m == "object" && !(m instanceof RegExp))
        throw Error('Invalid value for "path" argument - strings must start with / or *');
      const { pattern: $, keys: R } = ot(m);
      this.path = m, typeof h == "object" && h._sveltesparouter === !0 ? (this.component = h.component, this.conditions = h.conditions || [], this.userData = h.userData, this.props = h.props || {}) : (this.component = () => Promise.resolve(h), this.conditions = [], this.props = {}), this._pattern = $, this._keys = R;
    }
    /**
    * Checks if `path` matches the current route.
    * If there's a match, will return the list of parameters from the URL (if any).
    * In case of no match, the method will return `null`.
    *
    * @param {string} path - Path to test
    * @returns {null|Object.<string, string>} List of paramters from the URL if there's a match, or `null` otherwise.
    */
    match(m) {
      if (r) {
        if (typeof r == "string")
          if (m.startsWith(r))
            m = m.substr(r.length) || "/";
          else
            return null;
        else if (r instanceof RegExp) {
          const w = m.match(r);
          if (w && w[0])
            m = m.substr(w[0].length) || "/";
          else
            return null;
        }
      }
      const h = this._pattern.exec(m);
      if (h === null)
        return null;
      if (this._keys === !1)
        return h;
      const $ = {};
      let R = 0;
      for (; R < this._keys.length; ) {
        try {
          $[this._keys[R]] = decodeURIComponent(h[R + 1] || "") || null;
        } catch {
          $[this._keys[R]] = null;
        }
        R++;
      }
      return $;
    }
    /**
    * Dictionary with route details passed to the pre-conditions functions, as well as the `routeLoading`, `routeLoaded` and `conditionsFailed` events
    * @typedef {Object} RouteDetail
    * @property {string|RegExp} route - Route matched as defined in the route definition (could be a string or a reguar expression object)
    * @property {string} location - Location path
    * @property {string} querystring - Querystring from the hash
    * @property {object} [userData] - Custom data passed by the user
    * @property {SvelteComponent} [component] - Svelte component (only in `routeLoaded` events)
    * @property {string} [name] - Name of the Svelte component (only in `routeLoaded` events)
    */
    /**
    * Executes all conditions (if any) to control whether the route can be shown. Conditions are executed in the order they are defined, and if a condition fails, the following ones aren't executed.
    * 
    * @param {RouteDetail} detail - Route detail
    * @returns {boolean} Returns true if all the conditions succeeded
    */
    async checkConditions(m) {
      for (let h = 0; h < this.conditions.length; h++)
        if (!await this.conditions[h](m))
          return !1;
      return !0;
    }
  }
  const l = [];
  s instanceof Map ? s.forEach((_, m) => {
    l.push(new i(m, _));
  }) : Object.keys(s).forEach((_) => {
    l.push(new i(_, s[_]));
  });
  let o = null, u = null, f = {};
  const g = Me();
  async function y(_, m) {
    await st(), g(_, m);
  }
  let v = null, N = null;
  c && (N = (_) => {
    _.state && (_.state.__svelte_spa_router_scrollY || _.state.__svelte_spa_router_scrollX) ? v = _.state : v = null;
  }, window.addEventListener("popstate", N), tt(() => {
    at(v);
  }));
  let q = null, L = null;
  const x = $e.subscribe(async (_) => {
    q = _;
    let m = 0;
    for (; m < l.length; ) {
      const h = l[m].match(_.location);
      if (!h) {
        m++;
        continue;
      }
      const $ = {
        route: l[m].path,
        location: _.location,
        querystring: _.querystring,
        userData: l[m].userData,
        params: h && typeof h == "object" && Object.keys(h).length ? h : null
      };
      if (!await l[m].checkConditions($)) {
        n(0, o = null), L = null, y("conditionsFailed", $);
        return;
      }
      y("routeLoading", Object.assign({}, $));
      const R = l[m].component;
      if (L != R) {
        R.loading ? (n(0, o = R.loading), L = R, n(1, u = R.loadingParams), n(2, f = {}), y("routeLoaded", Object.assign({}, $, {
          component: o,
          name: o.name,
          params: u
        }))) : (n(0, o = null), L = null);
        const w = await R();
        if (_ != q)
          return;
        n(0, o = w && w.default || w), L = R;
      }
      h && typeof h == "object" && Object.keys(h).length ? n(1, u = h) : n(1, u = null), n(2, f = l[m].props), y("routeLoaded", Object.assign({}, $, {
        component: o,
        name: o.name,
        params: u
      })).then(() => {
        Oe.set(u);
      });
      return;
    }
    n(0, o = null), L = null, Oe.set(void 0);
  });
  nt(() => {
    x(), N && window.removeEventListener("popstate", N);
  });
  function P(_) {
    Ee.call(this, t, _);
  }
  function T(_) {
    Ee.call(this, t, _);
  }
  return t.$$set = (_) => {
    "routes" in _ && n(3, s = _.routes), "prefix" in _ && n(4, r = _.prefix), "restoreScrollState" in _ && n(5, c = _.restoreScrollState);
  }, t.$$.update = () => {
    t.$$.dirty & /*restoreScrollState*/
    32 && (history.scrollRestoration = c ? "manual" : "auto");
  }, [
    o,
    u,
    f,
    s,
    r,
    c,
    P,
    T
  ];
}
class dt extends te {
  constructor(e) {
    super(), ee(this, e, ft, ut, Y, {
      routes: 3,
      prefix: 4,
      restoreScrollState: 5
    });
  }
}
const ht = async function(t) {
  return await (await fetch("http://localhost:5266/api/Requirements", {
    headers: { accept: "*/*", "content-type": "application/json; charset=utf-8" },
    method: "POST",
    body: JSON.stringify(t)
  })).json();
}, We = async function() {
  return await (await fetch("http://localhost:5266/api/Requirements", { method: "GET" })).json();
}, pt = async function(t) {
  return await (await fetch("http://localhost:5266/api/Requirements/" + t, { method: "GET" })).json();
};
function Ce(t, e, n) {
  const s = t.slice();
  return s[3] = e[n], s;
}
function Le(t) {
  let e, n, s, r = (
    /*item*/
    t[3].name + ""
  ), c, i, l, o;
  function u() {
    return (
      /*click_handler*/
      t[2](
        /*item*/
        t[3]
      )
    );
  }
  return {
    c() {
      e = p("tr"), n = p("td"), s = p("a"), c = G(r), i = j(), b(s, "href", "javascript:;"), b(n, "class", "title");
    },
    m(f, g) {
      E(f, e, g), a(e, n), a(n, s), a(s, c), a(e, i), l || (o = Q(s, "click", u), l = !0);
    },
    p(f, g) {
      t = f, g & /*Requirements*/
      1 && r !== (r = /*item*/
      t[3].name + "") && le(c, r);
    },
    d(f) {
      f && k(e), l = !1, o();
    }
  };
}
function _t(t) {
  let e, n = (
    /*Requirements*/
    t[0]
  ), s = [];
  for (let r = 0; r < n.length; r += 1)
    s[r] = Le(Ce(t, n, r));
  return {
    c() {
      for (let r = 0; r < s.length; r += 1)
        s[r].c();
      e = ae();
    },
    m(r, c) {
      for (let i = 0; i < s.length; i += 1)
        s[i].m(r, c);
      E(r, e, c);
    },
    p(r, [c]) {
      if (c & /*dispatch, getRequirementAsync, Requirements*/
      3) {
        n = /*Requirements*/
        r[0];
        let i;
        for (i = 0; i < n.length; i += 1) {
          const l = Ce(r, n, i);
          s[i] ? s[i].p(l, c) : (s[i] = Le(l), s[i].c(), s[i].m(e.parentNode, e));
        }
        for (; i < s.length; i += 1)
          s[i].d(1);
        s.length = n.length;
      }
    },
    i: O,
    o: O,
    d(r) {
      be(s, r), r && k(e);
    }
  };
}
function mt(t, e, n) {
  let { Requirements: s } = e, r = Me();
  const c = async (i) => {
    r("showDetail", await pt(i.name));
  };
  return t.$$set = (i) => {
    "Requirements" in i && n(0, s = i.Requirements);
  }, [s, r, c];
}
class gt extends te {
  constructor(e) {
    super(), ee(this, e, mt, _t, Y, { Requirements: 0 });
  }
}
function bt(t) {
  let e, n, s, r, c, i, l, o, u, f, g, y, v, N, q, L, x, P, T, _, m, h, $, R, w = JSON.stringify(
    /*detail*/
    t[2]
  ).replaceAll('"', "") + "", W, J, M, K;
  return T = new gt({
    props: { Requirements: (
      /*Requirements*/
      t[0]
    ) }
  }), T.$on(
    "showDetail",
    /*showDetail*/
    t[4]
  ), {
    c() {
      e = p("div"), n = p("div"), s = p("div"), r = p("div"), r.innerHTML = '<h5 class="card-title">Requirements:</h5>', c = j(), i = p("div"), i.textContent = "Name:", l = j(), o = p("input"), u = j(), f = p("button"), f.textContent = "Submit", g = j(), y = p("div"), v = p("div"), N = p("div"), q = p("table"), L = p("thead"), L.innerHTML = "<tr><th>Name</th></tr>", x = j(), P = p("tbody"), Z(T.$$.fragment), _ = j(), m = p("div"), h = p("div"), $ = p("div"), R = p("h2"), W = G(w), b(r, "class", "card-header"), b(i, "class", "form-label"), b(o, "type", "text"), b(o, "class", "form-control"), b(o, "id", "requirementName"), b(f, "class", "btn btn-primary"), b(q, "class", "table table-sm data-table localesList"), b(N, "class", "col-12"), b(v, "class", "row"), b(y, "class", "card-body p-0"), b(s, "class", "card card-primary card-outline"), b(n, "class", "col-lg-3 col-md-12"), b($, "class", "card-body p-0"), b($, "id", "detailRequirement"), b(h, "class", "card card-primary card-outline card-tabs"), b(m, "class", "col-lg-9 col-md-12 svelte-7adzez"), b(e, "class", "row");
    },
    m(C, F) {
      E(C, e, F), a(e, n), a(n, s), a(s, r), a(s, c), a(s, i), a(s, l), a(s, o), oe(
        o,
        /*text*/
        t[1]
      ), a(s, u), a(s, f), a(s, g), a(s, y), a(y, v), a(v, N), a(N, q), a(q, L), a(q, x), a(q, P), B(T, P, null), a(e, _), a(e, m), a(m, h), a(h, $), a($, R), a(R, W), J = !0, M || (K = [
        Q(
          o,
          "input",
          /*input_input_handler*/
          t[5]
        ),
        Q(
          f,
          "click",
          /*addRequirement*/
          t[3]
        )
      ], M = !0);
    },
    p(C, [F]) {
      F & /*text*/
      2 && o.value !== /*text*/
      C[1] && oe(
        o,
        /*text*/
        C[1]
      );
      const z = {};
      F & /*Requirements*/
      1 && (z.Requirements = /*Requirements*/
      C[0]), T.$set(z), (!J || F & /*detail*/
      4) && w !== (w = JSON.stringify(
        /*detail*/
        C[2]
      ).replaceAll('"', "") + "") && le(W, w);
    },
    i(C) {
      J || (I(T.$$.fragment, C), J = !0);
    },
    o(C) {
      H(T.$$.fragment, C), J = !1;
    },
    d(C) {
      C && k(e), U(T), M = !1, X(K);
    }
  };
}
function yt(t, e, n) {
  const s = async function() {
    n(0, r = await We());
  };
  let r = [];
  s();
  let c = "";
  const i = async function() {
    await ht(c), n(1, c = ""), s();
  };
  let l = "";
  const o = async function(f) {
    console.log(f.detail), n(2, l = f.detail);
  };
  function u() {
    c = this.value, n(1, c);
  }
  return [r, c, l, i, o, u];
}
class vt extends te {
  constructor(e) {
    super(), ee(this, e, yt, bt, Y, {});
  }
}
function Ae(t, e, n) {
  const s = t.slice();
  return s[1] = e[n], s;
}
function Te(t) {
  let e, n, s = JSON.stringify(
    /*item*/
    t[1].name
  ) + "", r, c, i = JSON.stringify(
    /*item*/
    t[1].requirementList[0].requirement.name
  ) + "", l, o, u = JSON.stringify(
    /*item*/
    t[1].requirementList[0].amount
  ) + "", f;
  return {
    c() {
      e = p("tr"), n = p("td"), r = G(s), c = p("td"), l = G(i), o = p("td"), f = G(u);
    },
    m(g, y) {
      E(g, e, y), a(e, n), a(n, r), a(e, c), a(c, l), a(e, o), a(o, f);
    },
    p(g, y) {
      y & /*Resources*/
      1 && s !== (s = JSON.stringify(
        /*item*/
        g[1].name
      ) + "") && le(r, s), y & /*Resources*/
      1 && i !== (i = JSON.stringify(
        /*item*/
        g[1].requirementList[0].requirement.name
      ) + "") && le(l, i), y & /*Resources*/
      1 && u !== (u = JSON.stringify(
        /*item*/
        g[1].requirementList[0].amount
      ) + "") && le(f, u);
    },
    d(g) {
      g && k(e);
    }
  };
}
function wt(t) {
  let e, n = (
    /*Resources*/
    t[0]
  ), s = [];
  for (let r = 0; r < n.length; r += 1)
    s[r] = Te(Ae(t, n, r));
  return {
    c() {
      for (let r = 0; r < s.length; r += 1)
        s[r].c();
      e = ae();
    },
    m(r, c) {
      for (let i = 0; i < s.length; i += 1)
        s[i].m(r, c);
      E(r, e, c);
    },
    p(r, [c]) {
      if (c & /*JSON, Resources*/
      1) {
        n = /*Resources*/
        r[0];
        let i;
        for (i = 0; i < n.length; i += 1) {
          const l = Ae(r, n, i);
          s[i] ? s[i].p(l, c) : (s[i] = Te(l), s[i].c(), s[i].m(e.parentNode, e));
        }
        for (; i < s.length; i += 1)
          s[i].d(1);
        s.length = n.length;
      }
    },
    i: O,
    o: O,
    d(r) {
      be(s, r), r && k(e);
    }
  };
}
function $t(t, e, n) {
  let { Resources: s } = e;
  return t.$$set = (r) => {
    "Resources" in r && n(0, s = r.Resources);
  }, [s];
}
class Rt extends te {
  constructor(e) {
    super(), ee(this, e, $t, wt, Y, { Resources: 0 });
  }
}
const kt = async function(t, e, n) {
  var s = {};
  return s.ResourceName = t, s.RequirementName = e, s.RequirementCapacity = n, console.log(s), await (await fetch("http://localhost:5266/api/Resource", {
    headers: { accept: "*/*", "content-type": "application/json; charset=utf-8" },
    method: "POST",
    body: JSON.stringify(s)
  })).json();
}, qt = async function() {
  return await (await fetch("http://localhost:5266/api/Resource", { method: "GET" })).json();
};
function xe(t, e, n) {
  const s = t.slice();
  return s[11] = e[n], s;
}
function De(t) {
  let e, n = (
    /*item*/
    t[11].name + ""
  ), s, r;
  return {
    c() {
      e = p("option"), s = G(n), e.__value = r = /*item*/
      t[11].name, e.value = e.__value;
    },
    m(c, i) {
      E(c, e, i), a(e, s);
    },
    p(c, i) {
      i & /*Requirements*/
      2 && n !== (n = /*item*/
      c[11].name + "") && le(s, n), i & /*Requirements*/
      2 && r !== (r = /*item*/
      c[11].name) && (e.__value = r, e.value = e.__value);
    },
    d(c) {
      c && k(e);
    }
  };
}
function Et(t) {
  let e, n, s, r, c, i, l, o, u, f, g, y, v, N, q, L, x, P, T, _, m, h, $, R, w, W, J, M, K, C, F;
  f = new Rt({
    props: { Resources: (
      /*Resources*/
      t[0]
    ) }
  });
  let z = (
    /*Requirements*/
    t[1]
  ), A = [];
  for (let d = 0; d < z.length; d += 1)
    A[d] = De(xe(t, z, d));
  return {
    c() {
      e = p("h1"), e.textContent = "Resources", n = j(), s = p("table"), r = p("tr"), r.innerHTML = "<th>Resource Name</th>", c = j(), i = p("tr"), i.innerHTML = "<th>Requirement Name</th>", l = j(), o = p("tr"), o.innerHTML = "<th>Requirement Capacity</th>", u = j(), Z(f.$$.fragment), g = j(), y = p("div"), v = p("div"), v.textContent = "Name:", N = j(), q = p("input"), L = j(), x = p("div"), P = p("div"), P.textContent = "Requirement Name:", T = j(), _ = p("select");
      for (let d = 0; d < A.length; d += 1)
        A[d].c();
      m = j(), h = p("div"), $ = p("div"), $.textContent = "Requirement Amount:", R = j(), w = p("input"), W = j(), J = p("div"), M = p("button"), M.textContent = "Submit", b(v, "class", "form-label"), b(q, "type", "text"), b(q, "class", "form-control"), b(q, "id", "resourceName"), b(y, "class", "mb-3"), b(P, "class", "form-label"), /*selected*/
      t[2] === void 0 && pe(() => (
        /*select_change_handler*/
        t[7].call(_)
      )), b(x, "class", "mb-3"), b($, "class", "form-label"), b(w, "type", "number"), b(w, "class", "form-control"), b(w, "id", "requirementCapacity"), b(h, "class", "mb-3"), b(M, "class", "btn btn-primary"), b(J, "class", "mb-3");
    },
    m(d, S) {
      E(d, e, S), E(d, n, S), E(d, s, S), a(s, r), a(s, c), a(s, i), a(s, l), a(s, o), a(s, u), B(f, s, null), E(d, g, S), E(d, y, S), a(y, v), a(y, N), a(y, q), oe(
        q,
        /*resourceNameInput*/
        t[3]
      ), E(d, L, S), E(d, x, S), a(x, P), a(x, T), a(x, _);
      for (let ne = 0; ne < A.length; ne += 1)
        A[ne].m(_, null);
      qe(
        _,
        /*selected*/
        t[2]
      ), E(d, m, S), E(d, h, S), a(h, $), a(h, R), a(h, w), oe(
        w,
        /*resourceCapacityInput*/
        t[4]
      ), E(d, W, S), E(d, J, S), a(J, M), K = !0, C || (F = [
        Q(
          q,
          "input",
          /*input0_input_handler*/
          t[6]
        ),
        Q(
          _,
          "change",
          /*select_change_handler*/
          t[7]
        ),
        Q(
          w,
          "input",
          /*input1_input_handler*/
          t[8]
        ),
        Q(
          M,
          "click",
          /*addResource*/
          t[5]
        )
      ], C = !0);
    },
    p(d, [S]) {
      const ne = {};
      if (S & /*Resources*/
      1 && (ne.Resources = /*Resources*/
      d[0]), f.$set(ne), S & /*resourceNameInput*/
      8 && q.value !== /*resourceNameInput*/
      d[3] && oe(
        q,
        /*resourceNameInput*/
        d[3]
      ), S & /*Requirements*/
      2) {
        z = /*Requirements*/
        d[1];
        let D;
        for (D = 0; D < z.length; D += 1) {
          const Re = xe(d, z, D);
          A[D] ? A[D].p(Re, S) : (A[D] = De(Re), A[D].c(), A[D].m(_, null));
        }
        for (; D < A.length; D += 1)
          A[D].d(1);
        A.length = z.length;
      }
      S & /*selected, Requirements*/
      6 && qe(
        _,
        /*selected*/
        d[2]
      ), S & /*resourceCapacityInput*/
      16 && Ie(w.value) !== /*resourceCapacityInput*/
      d[4] && oe(
        w,
        /*resourceCapacityInput*/
        d[4]
      );
    },
    i(d) {
      K || (I(f.$$.fragment, d), K = !0);
    },
    o(d) {
      H(f.$$.fragment, d), K = !1;
    },
    d(d) {
      d && k(e), d && k(n), d && k(s), U(f), d && k(g), d && k(y), d && k(L), d && k(x), be(A, d), d && k(m), d && k(h), d && k(W), d && k(J), C = !1, X(F);
    }
  };
}
function Nt(t, e, n) {
  const s = async function() {
    n(0, r = await qt());
  };
  let r = [];
  s();
  const c = async function() {
    n(1, i = await We());
  };
  let i = [];
  c();
  let l, o = "", u = 0;
  const f = async function() {
    await kt(o, l, u), n(3, o = ""), n(4, u = 0), s();
  };
  function g() {
    o = this.value, n(3, o);
  }
  function y() {
    l = Ze(this), n(2, l), n(1, i);
  }
  function v() {
    u = Ie(this.value), n(4, u);
  }
  return [
    r,
    i,
    l,
    o,
    u,
    f,
    g,
    y,
    v
  ];
}
class St extends te {
  constructor(e) {
    super(), ee(this, e, Nt, Et, Y, {});
  }
}
function jt(t) {
  let e;
  return {
    c() {
      e = p("h1"), e.textContent = "Page not found lol";
    },
    m(n, s) {
      E(n, e, s);
    },
    p: O,
    i: O,
    o: O,
    d(n) {
      n && k(e);
    }
  };
}
class Ot extends te {
  constructor(e) {
    super(), ee(this, e, null, jt, Y, {});
  }
}
const Ct = {
  "/requirements": vt,
  "/resources": St,
  "*": Ot
};
function Lt(t) {
  let e, n;
  return e = new dt({ props: { routes: Ct } }), {
    c() {
      Z(e.$$.fragment);
    },
    m(s, r) {
      B(e, s, r), n = !0;
    },
    p: O,
    i(s) {
      n || (I(e.$$.fragment, s), n = !0);
    },
    o(s) {
      H(e.$$.fragment, s), n = !1;
    },
    d(s) {
      U(e, s);
    }
  };
}
class At extends te {
  constructor(e) {
    super(), ee(this, e, null, Lt, Y, {});
  }
}
const Tt = new At({
  target: document.getElementById("app")
});
export {
  Tt as default
};
//# sourceMappingURL=main.js.map
