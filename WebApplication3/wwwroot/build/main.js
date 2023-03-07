function O() {
}
function Me(t, e) {
  for (const n in e)
    t[n] = e[n];
  return t;
}
function Ie(t) {
  return t();
}
function $e() {
  return /* @__PURE__ */ Object.create(null);
}
function X(t) {
  t.forEach(Ie);
}
function be(t) {
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
function R(t, e, n) {
  t.insertBefore(e, n || null);
}
function k(t) {
  t.parentNode && t.parentNode.removeChild(t);
}
function ge(t, e) {
  for (let n = 0; n < t.length; n += 1)
    t[n] && t[n].d(e);
}
function m(t) {
  return document.createElement(t);
}
function F(t) {
  return document.createTextNode(t);
}
function S() {
  return F(" ");
}
function ae() {
  return F("");
}
function Q(t, e, n, r) {
  return t.addEventListener(e, n, r), () => t.removeEventListener(e, n, r);
}
function b(t, e, n) {
  n == null ? t.removeAttribute(e) : t.getAttribute(e) !== n && t.setAttribute(e, n);
}
function He(t) {
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
function Ee(t, e) {
  for (let n = 0; n < t.options.length; n += 1) {
    const r = t.options[n];
    if (r.__value === e) {
      r.selected = !0;
      return;
    }
  }
  t.selectedIndex = -1;
}
function Ze(t) {
  const e = t.querySelector(":checked") || t.options[0];
  return e && e.__value;
}
function et(t, e, { bubbles: n = !1, cancelable: r = !1 } = {}) {
  const s = document.createEvent("CustomEvent");
  return s.initCustomEvent(t, n, r, e), s;
}
function me(t, e) {
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
function rt() {
  const t = ye();
  return (e, n, { cancelable: r = !1 } = {}) => {
    const s = t.$$.callbacks[e];
    if (s) {
      const l = et(e, n, { cancelable: r });
      return s.slice().forEach((i) => {
        i.call(t, l);
      }), !l.defaultPrevented;
    }
    return !0;
  };
}
function Ne(t, e) {
  const n = t.$$.callbacks[e.type];
  n && n.slice().forEach((r) => r.call(this, e));
}
const ie = [], Se = [], fe = [], je = [], Ue = Promise.resolve();
let _e = !1;
function ze() {
  _e || (_e = !0, Ue.then(Fe));
}
function st() {
  return ze(), Ue;
}
function pe(t) {
  fe.push(t);
}
const he = /* @__PURE__ */ new Set();
let re = 0;
function Fe() {
  if (re !== 0)
    return;
  const t = ue;
  do {
    try {
      for (; re < ie.length; ) {
        const e = ie[re];
        re++, ce(e), it(e.$$);
      }
    } catch (e) {
      throw ie.length = 0, re = 0, e;
    }
    for (ce(null), ie.length = 0, re = 0; Se.length; )
      Se.pop()();
    for (let e = 0; e < fe.length; e += 1) {
      const n = fe[e];
      he.has(n) || (he.add(n), n());
    }
    fe.length = 0;
  } while (ie.length);
  for (; je.length; )
    je.pop()();
  _e = !1, he.clear(), ce(t);
}
function it(t) {
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
function M(t, e) {
  t && t.i && (de.delete(t), t.i(e));
}
function U(t, e, n, r) {
  if (t && t.o) {
    if (de.has(t))
      return;
    de.add(t), V.c.push(() => {
      de.delete(t), r && (n && t.d(1), r());
    }), t.o(e);
  } else
    r && r();
}
function Ge(t, e) {
  const n = {}, r = {}, s = { $$scope: 1 };
  let l = t.length;
  for (; l--; ) {
    const i = t[l], c = e[l];
    if (c) {
      for (const o in i)
        o in c || (r[o] = 1);
      for (const o in c)
        s[o] || (n[o] = c[o], s[o] = 1);
      t[l] = c;
    } else
      for (const o in i)
        s[o] = 1;
  }
  for (const i in r)
    i in n || (n[i] = void 0);
  return n;
}
function Be(t) {
  return typeof t == "object" && t !== null ? t : {};
}
function Z(t) {
  t && t.c();
}
function G(t, e, n, r) {
  const { fragment: s, after_update: l } = t.$$;
  s && s.m(e, n), r || pe(() => {
    const i = t.$$.on_mount.map(Ie).filter(be);
    t.$$.on_destroy ? t.$$.on_destroy.push(...i) : X(i), t.$$.on_mount = [];
  }), l.forEach(pe);
}
function B(t, e) {
  const n = t.$$;
  n.fragment !== null && (X(n.on_destroy), n.fragment && n.fragment.d(e), n.on_destroy = n.fragment = null, n.ctx = []);
}
function ot(t, e) {
  t.$$.dirty[0] === -1 && (ie.push(t), ze(), t.$$.dirty.fill(0)), t.$$.dirty[e / 31 | 0] |= 1 << e % 31;
}
function ee(t, e, n, r, s, l, i, c = [-1]) {
  const o = ue;
  ce(t);
  const u = t.$$ = {
    fragment: null,
    ctx: [],
    // state
    props: l,
    update: O,
    not_equal: s,
    bound: $e(),
    // lifecycle
    on_mount: [],
    on_destroy: [],
    on_disconnect: [],
    before_update: [],
    after_update: [],
    context: new Map(e.context || (o ? o.$$.context : [])),
    // everything else
    callbacks: $e(),
    dirty: c,
    skip_bound: !1,
    root: e.target || o.$$.root
  };
  i && i(u.root);
  let f = !1;
  if (u.ctx = n ? n(t, e.props || {}, (g, y, ...v) => {
    const w = v.length ? v[0] : y;
    return u.ctx && s(u.ctx[g], u.ctx[g] = w) && (!u.skip_bound && u.bound[g] && u.bound[g](w), f && ot(t, g)), y;
  }) : [], u.update(), f = !0, X(u.before_update), u.fragment = r ? r(u.ctx) : !1, e.target) {
    if (e.hydrate) {
      const g = Ve(e.target);
      u.fragment && u.fragment.l(g), g.forEach(k);
    } else
      u.fragment && u.fragment.c();
    e.intro && M(t.$$.fragment), G(t, e.target, e.anchor, e.customElement), Fe();
  }
  ce(o);
}
class te {
  $destroy() {
    B(this, 1), this.$destroy = O;
  }
  $on(e, n) {
    if (!be(n))
      return O;
    const r = this.$$.callbacks[e] || (this.$$.callbacks[e] = []);
    return r.push(n), () => {
      const s = r.indexOf(n);
      s !== -1 && r.splice(s, 1);
    };
  }
  $set(e) {
    this.$$set && !Ke(e) && (this.$$.skip_bound = !0, this.$$set(e), this.$$.skip_bound = !1);
  }
}
const se = [];
function Xe(t, e) {
  return {
    subscribe: Ye(t, e).subscribe
  };
}
function Ye(t, e = O) {
  let n;
  const r = /* @__PURE__ */ new Set();
  function s(c) {
    if (Y(t, c) && (t = c, n)) {
      const o = !se.length;
      for (const u of r)
        u[1](), se.push(u, t);
      if (o) {
        for (let u = 0; u < se.length; u += 2)
          se[u][0](se[u + 1]);
        se.length = 0;
      }
    }
  }
  function l(c) {
    s(c(t));
  }
  function i(c, o = O) {
    const u = [c, o];
    return r.add(u), r.size === 1 && (n = e(s) || O), c(t), () => {
      r.delete(u), r.size === 0 && (n(), n = null);
    };
  }
  return { set: s, update: l, subscribe: i };
}
function We(t, e, n) {
  const r = !Array.isArray(t), s = r ? [t] : t, l = e.length < 2;
  return Xe(n, (i) => {
    let c = !1;
    const o = [];
    let u = 0, f = O;
    const g = () => {
      if (u)
        return;
      f();
      const v = e(r ? o[0] : o, i);
      l ? i(v) : f = be(v) ? v : O;
    }, y = s.map((v, w) => Qe(v, (N) => {
      o[w] = N, u &= ~(1 << w), c && g();
    }, () => {
      u |= 1 << w;
    }));
    return c = !0, g(), function() {
      X(y), f();
    };
  });
}
function lt(t, e) {
  if (t instanceof RegExp)
    return { keys: !1, pattern: t };
  var n, r, s, l, i = [], c = "", o = t.split("/");
  for (o[0] || o.shift(); s = o.shift(); )
    n = s[0], n === "*" ? (i.push("wild"), c += "/(.*)") : n === ":" ? (r = s.indexOf("?", 1), l = s.indexOf(".", 1), i.push(s.substring(1, ~r ? r : ~l ? l : s.length)), c += ~r && !~l ? "(?:/([^/]+?))?" : "/([^/]+?)", ~l && (c += (~r ? "?" : "") + "\\" + s.substring(l))) : c += "/" + s;
  return {
    keys: i,
    pattern: new RegExp("^" + c + (e ? "(?=$|/)" : "/?$"), "i")
  };
}
function ct(t) {
  let e, n, r;
  const s = [
    /*props*/
    t[2]
  ];
  var l = (
    /*component*/
    t[0]
  );
  function i(c) {
    let o = {};
    for (let u = 0; u < s.length; u += 1)
      o = Me(o, s[u]);
    return { props: o };
  }
  return l && (e = me(l, i()), e.$on(
    "routeEvent",
    /*routeEvent_handler_1*/
    t[7]
  )), {
    c() {
      e && Z(e.$$.fragment), n = ae();
    },
    m(c, o) {
      e && G(e, c, o), R(c, n, o), r = !0;
    },
    p(c, o) {
      const u = o & /*props*/
      4 ? Ge(s, [Be(
        /*props*/
        c[2]
      )]) : {};
      if (l !== (l = /*component*/
      c[0])) {
        if (e) {
          ve();
          const f = e;
          U(f.$$.fragment, 1, 0, () => {
            B(f, 1);
          }), we();
        }
        l ? (e = me(l, i()), e.$on(
          "routeEvent",
          /*routeEvent_handler_1*/
          c[7]
        ), Z(e.$$.fragment), M(e.$$.fragment, 1), G(e, n.parentNode, n)) : e = null;
      } else
        l && e.$set(u);
    },
    i(c) {
      r || (e && M(e.$$.fragment, c), r = !0);
    },
    o(c) {
      e && U(e.$$.fragment, c), r = !1;
    },
    d(c) {
      c && k(n), e && B(e, c);
    }
  };
}
function ut(t) {
  let e, n, r;
  const s = [
    { params: (
      /*componentParams*/
      t[1]
    ) },
    /*props*/
    t[2]
  ];
  var l = (
    /*component*/
    t[0]
  );
  function i(c) {
    let o = {};
    for (let u = 0; u < s.length; u += 1)
      o = Me(o, s[u]);
    return { props: o };
  }
  return l && (e = me(l, i()), e.$on(
    "routeEvent",
    /*routeEvent_handler*/
    t[6]
  )), {
    c() {
      e && Z(e.$$.fragment), n = ae();
    },
    m(c, o) {
      e && G(e, c, o), R(c, n, o), r = !0;
    },
    p(c, o) {
      const u = o & /*componentParams, props*/
      6 ? Ge(s, [
        o & /*componentParams*/
        2 && { params: (
          /*componentParams*/
          c[1]
        ) },
        o & /*props*/
        4 && Be(
          /*props*/
          c[2]
        )
      ]) : {};
      if (l !== (l = /*component*/
      c[0])) {
        if (e) {
          ve();
          const f = e;
          U(f.$$.fragment, 1, 0, () => {
            B(f, 1);
          }), we();
        }
        l ? (e = me(l, i()), e.$on(
          "routeEvent",
          /*routeEvent_handler*/
          c[6]
        ), Z(e.$$.fragment), M(e.$$.fragment, 1), G(e, n.parentNode, n)) : e = null;
      } else
        l && e.$set(u);
    },
    i(c) {
      r || (e && M(e.$$.fragment, c), r = !0);
    },
    o(c) {
      e && U(e.$$.fragment, c), r = !1;
    },
    d(c) {
      c && k(n), e && B(e, c);
    }
  };
}
function at(t) {
  let e, n, r, s;
  const l = [ut, ct], i = [];
  function c(o, u) {
    return (
      /*componentParams*/
      o[1] ? 0 : 1
    );
  }
  return e = c(t), n = i[e] = l[e](t), {
    c() {
      n.c(), r = ae();
    },
    m(o, u) {
      i[e].m(o, u), R(o, r, u), s = !0;
    },
    p(o, [u]) {
      let f = e;
      e = c(o), e === f ? i[e].p(o, u) : (ve(), U(i[f], 1, 1, () => {
        i[f] = null;
      }), we(), n = i[e], n ? n.p(o, u) : (n = i[e] = l[e](o), n.c()), M(n, 1), n.m(r.parentNode, r));
    },
    i(o) {
      s || (M(n), s = !0);
    },
    o(o) {
      U(n), s = !1;
    },
    d(o) {
      i[e].d(o), o && k(r);
    }
  };
}
function Oe() {
  const t = window.location.href.indexOf("#/");
  let e = t > -1 ? window.location.href.substr(t + 1) : "/";
  const n = e.indexOf("?");
  let r = "";
  return n > -1 && (r = e.substr(n + 1), e = e.substr(0, n)), { location: e, querystring: r };
}
const ke = Xe(
  null,
  // eslint-disable-next-line prefer-arrow-callback
  function(e) {
    e(Oe());
    const n = () => {
      e(Oe());
    };
    return window.addEventListener("hashchange", n, !1), function() {
      window.removeEventListener("hashchange", n, !1);
    };
  }
);
We(ke, (t) => t.location);
We(ke, (t) => t.querystring);
const Le = Ye(void 0);
function ft(t) {
  t ? window.scrollTo(t.__svelte_spa_router_scrollX, t.__svelte_spa_router_scrollY) : window.scrollTo(0, 0);
}
function dt(t, e, n) {
  let { routes: r = {} } = e, { prefix: s = "" } = e, { restoreScrollState: l = !1 } = e;
  class i {
    /**
    * Initializes the object and creates a regular expression from the path, using regexparam.
    *
    * @param {string} path - Path to the route (must start with '/' or '*')
    * @param {SvelteComponent|WrappedComponent} component - Svelte component for the route, optionally wrapped
    */
    constructor(_, p) {
      if (!p || typeof p != "function" && (typeof p != "object" || p._sveltesparouter !== !0))
        throw Error("Invalid component object");
      if (!_ || typeof _ == "string" && (_.length < 1 || _.charAt(0) != "/" && _.charAt(0) != "*") || typeof _ == "object" && !(_ instanceof RegExp))
        throw Error('Invalid value for "path" argument - strings must start with / or *');
      const { pattern: $, keys: E } = lt(_);
      this.path = _, typeof p == "object" && p._sveltesparouter === !0 ? (this.component = p.component, this.conditions = p.conditions || [], this.userData = p.userData, this.props = p.props || {}) : (this.component = () => Promise.resolve(p), this.conditions = [], this.props = {}), this._pattern = $, this._keys = E;
    }
    /**
    * Checks if `path` matches the current route.
    * If there's a match, will return the list of parameters from the URL (if any).
    * In case of no match, the method will return `null`.
    *
    * @param {string} path - Path to test
    * @returns {null|Object.<string, string>} List of paramters from the URL if there's a match, or `null` otherwise.
    */
    match(_) {
      if (s) {
        if (typeof s == "string")
          if (_.startsWith(s))
            _ = _.substr(s.length) || "/";
          else
            return null;
        else if (s instanceof RegExp) {
          const q = _.match(s);
          if (q && q[0])
            _ = _.substr(q[0].length) || "/";
          else
            return null;
        }
      }
      const p = this._pattern.exec(_);
      if (p === null)
        return null;
      if (this._keys === !1)
        return p;
      const $ = {};
      let E = 0;
      for (; E < this._keys.length; ) {
        try {
          $[this._keys[E]] = decodeURIComponent(p[E + 1] || "") || null;
        } catch {
          $[this._keys[E]] = null;
        }
        E++;
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
    async checkConditions(_) {
      for (let p = 0; p < this.conditions.length; p++)
        if (!await this.conditions[p](_))
          return !1;
      return !0;
    }
  }
  const c = [];
  r instanceof Map ? r.forEach((h, _) => {
    c.push(new i(_, h));
  }) : Object.keys(r).forEach((h) => {
    c.push(new i(h, r[h]));
  });
  let o = null, u = null, f = {};
  const g = rt();
  async function y(h, _) {
    await st(), g(h, _);
  }
  let v = null, w = null;
  l && (w = (h) => {
    h.state && (h.state.__svelte_spa_router_scrollY || h.state.__svelte_spa_router_scrollX) ? v = h.state : v = null;
  }, window.addEventListener("popstate", w), tt(() => {
    ft(v);
  }));
  let N = null, A = null;
  const T = ke.subscribe(async (h) => {
    N = h;
    let _ = 0;
    for (; _ < c.length; ) {
      const p = c[_].match(h.location);
      if (!p) {
        _++;
        continue;
      }
      const $ = {
        route: c[_].path,
        location: h.location,
        querystring: h.querystring,
        userData: c[_].userData,
        params: p && typeof p == "object" && Object.keys(p).length ? p : null
      };
      if (!await c[_].checkConditions($)) {
        n(0, o = null), A = null, y("conditionsFailed", $);
        return;
      }
      y("routeLoading", Object.assign({}, $));
      const E = c[_].component;
      if (A != E) {
        E.loading ? (n(0, o = E.loading), A = E, n(1, u = E.loadingParams), n(2, f = {}), y("routeLoaded", Object.assign({}, $, {
          component: o,
          name: o.name,
          params: u
        }))) : (n(0, o = null), A = null);
        const q = await E();
        if (h != N)
          return;
        n(0, o = q && q.default || q), A = E;
      }
      p && typeof p == "object" && Object.keys(p).length ? n(1, u = p) : n(1, u = null), n(2, f = c[_].props), y("routeLoaded", Object.assign({}, $, {
        component: o,
        name: o.name,
        params: u
      })).then(() => {
        Le.set(u);
      });
      return;
    }
    n(0, o = null), A = null, Le.set(void 0);
  });
  nt(() => {
    T(), w && window.removeEventListener("popstate", w);
  });
  function P(h) {
    Ne.call(this, t, h);
  }
  function x(h) {
    Ne.call(this, t, h);
  }
  return t.$$set = (h) => {
    "routes" in h && n(3, r = h.routes), "prefix" in h && n(4, s = h.prefix), "restoreScrollState" in h && n(5, l = h.restoreScrollState);
  }, t.$$.update = () => {
    t.$$.dirty & /*restoreScrollState*/
    32 && (history.scrollRestoration = l ? "manual" : "auto");
  }, [
    o,
    u,
    f,
    r,
    s,
    l,
    P,
    x
  ];
}
class mt extends te {
  constructor(e) {
    super(), ee(this, e, dt, at, Y, {
      routes: 3,
      prefix: 4,
      restoreScrollState: 5
    });
  }
}
const pt = async function(t) {
  return await (await fetch(RequirementsApiUrl, {
    headers: { accept: "*/*", "content-type": "application/json; charset=utf-8" },
    method: "POST",
    body: JSON.stringify(t)
  })).json();
}, qe = async function() {
  return await (await fetch(RequirementsApiUrl, { method: "GET" })).json();
}, ht = async function(t) {
  return await (await fetch(RequirementsApiUrl + "/" + t, { method: "GET" })).json();
}, _t = async function(t) {
  return await (await fetch(RequirementsApiUrl + "/" + t, { method: "DELETE" })).json();
};
function Ae(t, e, n) {
  const r = t.slice();
  return r[2] = e[n], r;
}
function Ce(t) {
  let e, n, r, s = (
    /*item*/
    t[2].name + ""
  ), l, i, c, o, u, f, g;
  function y() {
    return (
      /*click_handler*/
      t[1](
        /*item*/
        t[2]
      )
    );
  }
  return {
    c() {
      e = m("tr"), n = m("td"), r = m("a"), l = F(s), c = S(), o = m("button"), o.innerHTML = '<i class="fa fa-trash"></i>', u = S(), b(r, "href", i = "#/requirements/" + /*item*/
      t[2].name), b(n, "class", "title"), b(o, "class", "btn btn-danger btn-sm rounded-0"), b(o, "type", "button"), b(o, "data-toggle", "tooltip"), b(o, "data-placement", "top"), b(o, "title", "Delete");
    },
    m(v, w) {
      R(v, e, w), a(e, n), a(n, r), a(r, l), a(e, c), a(e, o), a(e, u), f || (g = Q(o, "click", y), f = !0);
    },
    p(v, w) {
      t = v, w & /*Requirements*/
      1 && s !== (s = /*item*/
      t[2].name + "") && le(l, s), w & /*Requirements*/
      1 && i !== (i = "#/requirements/" + /*item*/
      t[2].name) && b(r, "href", i);
    },
    d(v) {
      v && k(e), f = !1, g();
    }
  };
}
function bt(t) {
  let e, n = (
    /*Requirements*/
    t[0]
  ), r = [];
  for (let s = 0; s < n.length; s += 1)
    r[s] = Ce(Ae(t, n, s));
  return {
    c() {
      for (let s = 0; s < r.length; s += 1)
        r[s].c();
      e = ae();
    },
    m(s, l) {
      for (let i = 0; i < r.length; i += 1)
        r[i].m(s, l);
      R(s, e, l);
    },
    p(s, [l]) {
      if (l & /*deleteRequirementAsync, Requirements, getRequirementsAsync*/
      1) {
        n = /*Requirements*/
        s[0];
        let i;
        for (i = 0; i < n.length; i += 1) {
          const c = Ae(s, n, i);
          r[i] ? r[i].p(c, l) : (r[i] = Ce(c), r[i].c(), r[i].m(e.parentNode, e));
        }
        for (; i < r.length; i += 1)
          r[i].d(1);
        r.length = n.length;
      }
    },
    i: O,
    o: O,
    d(s) {
      ge(r, s), s && k(e);
    }
  };
}
function gt(t, e, n) {
  let { Requirements: r } = e;
  const s = async (l) => {
    await _t(l.name), await qe();
  };
  return t.$$set = (l) => {
    "Requirements" in l && n(0, r = l.Requirements);
  }, [r, s];
}
class yt extends te {
  constructor(e) {
    super(), ee(this, e, gt, bt, Y, { Requirements: 0 });
  }
}
function vt(t) {
  let e, n, r, s, l, i, c, o, u, f, g, y, v, w, N, A, T, P, x, h, _, p, $, E, q = JSON.stringify(
    /*detail*/
    t[2]
  ).replaceAll('"', "") + "", W, J, I, K;
  return x = new yt({
    props: { Requirements: (
      /*Requirements*/
      t[0]
    ) }
  }), {
    c() {
      e = m("div"), n = m("div"), r = m("div"), s = m("div"), s.innerHTML = '<h5 class="card-title">Requirements:</h5>', l = S(), i = m("div"), i.textContent = "Name:", c = S(), o = m("input"), u = S(), f = m("button"), f.textContent = "Submit", g = S(), y = m("div"), v = m("div"), w = m("div"), N = m("table"), A = m("thead"), A.innerHTML = "<tr><th>Name</th></tr>", T = S(), P = m("tbody"), Z(x.$$.fragment), h = S(), _ = m("div"), p = m("div"), $ = m("div"), E = m("h2"), W = F(q), b(s, "class", "card-header"), b(i, "class", "form-label"), b(o, "type", "text"), b(o, "class", "form-control"), b(o, "id", "requirementName"), b(f, "class", "btn btn-primary"), b(N, "class", "table table-sm data-table localesList"), b(w, "class", "col-12"), b(v, "class", "row"), b(y, "class", "card-body p-0"), b(r, "class", "card card-primary card-outline"), b(n, "class", "col-lg-3 col-md-12"), b($, "class", "card-body p-0"), b($, "id", "detailRequirement"), b(p, "class", "card card-primary card-outline card-tabs"), b(_, "class", "col-lg-9 col-md-12 svelte-7adzez"), b(e, "class", "row");
    },
    m(L, z) {
      R(L, e, z), a(e, n), a(n, r), a(r, s), a(r, l), a(r, i), a(r, c), a(r, o), oe(
        o,
        /*text*/
        t[1]
      ), a(r, u), a(r, f), a(r, g), a(r, y), a(y, v), a(v, w), a(w, N), a(N, A), a(N, T), a(N, P), G(x, P, null), a(e, h), a(e, _), a(_, p), a(p, $), a($, E), a(E, W), J = !0, I || (K = [
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
      ], I = !0);
    },
    p(L, [z]) {
      z & /*text*/
      2 && o.value !== /*text*/
      L[1] && oe(
        o,
        /*text*/
        L[1]
      );
      const H = {};
      z & /*Requirements*/
      1 && (H.Requirements = /*Requirements*/
      L[0]), x.$set(H), (!J || z & /*detail*/
      4) && q !== (q = JSON.stringify(
        /*detail*/
        L[2]
      ).replaceAll('"', "") + "") && le(W, q);
    },
    i(L) {
      J || (M(x.$$.fragment, L), J = !0);
    },
    o(L) {
      U(x.$$.fragment, L), J = !1;
    },
    d(L) {
      L && k(e), B(x), I = !1, X(K);
    }
  };
}
function wt(t, e, n) {
  let { params: r } = e;
  const s = async function() {
    n(0, l = await qe());
  };
  let l = [];
  s();
  let i = "";
  const c = async function() {
    await pt(i), n(1, i = ""), s();
  };
  let o = "";
  async function u(g) {
    n(2, o = await ht(g));
  }
  function f() {
    i = this.value, n(1, i);
  }
  return t.$$set = (g) => {
    "params" in g && n(4, r = g.params);
  }, t.$$.update = () => {
    t.$$.dirty & /*params*/
    16 && u(r == null ? void 0 : r.reqName);
  }, [l, i, o, c, r, f];
}
class Te extends te {
  constructor(e) {
    super(), ee(this, e, wt, vt, Y, { params: 4 });
  }
}
function xe(t, e, n) {
  const r = t.slice();
  return r[1] = e[n], r;
}
function De(t) {
  let e, n, r = JSON.stringify(
    /*item*/
    t[1].name
  ) + "", s, l, i = JSON.stringify(
    /*item*/
    t[1].requirementList[0].requirement.name
  ) + "", c, o, u = JSON.stringify(
    /*item*/
    t[1].requirementList[0].amount
  ) + "", f;
  return {
    c() {
      e = m("tr"), n = m("td"), s = F(r), l = m("td"), c = F(i), o = m("td"), f = F(u);
    },
    m(g, y) {
      R(g, e, y), a(e, n), a(n, s), a(e, l), a(l, c), a(e, o), a(o, f);
    },
    p(g, y) {
      y & /*Resources*/
      1 && r !== (r = JSON.stringify(
        /*item*/
        g[1].name
      ) + "") && le(s, r), y & /*Resources*/
      1 && i !== (i = JSON.stringify(
        /*item*/
        g[1].requirementList[0].requirement.name
      ) + "") && le(c, i), y & /*Resources*/
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
function kt(t) {
  let e, n = (
    /*Resources*/
    t[0]
  ), r = [];
  for (let s = 0; s < n.length; s += 1)
    r[s] = De(xe(t, n, s));
  return {
    c() {
      for (let s = 0; s < r.length; s += 1)
        r[s].c();
      e = ae();
    },
    m(s, l) {
      for (let i = 0; i < r.length; i += 1)
        r[i].m(s, l);
      R(s, e, l);
    },
    p(s, [l]) {
      if (l & /*JSON, Resources*/
      1) {
        n = /*Resources*/
        s[0];
        let i;
        for (i = 0; i < n.length; i += 1) {
          const c = xe(s, n, i);
          r[i] ? r[i].p(c, l) : (r[i] = De(c), r[i].c(), r[i].m(e.parentNode, e));
        }
        for (; i < r.length; i += 1)
          r[i].d(1);
        r.length = n.length;
      }
    },
    i: O,
    o: O,
    d(s) {
      ge(r, s), s && k(e);
    }
  };
}
function qt(t, e, n) {
  let { Resources: r } = e;
  return t.$$set = (s) => {
    "Resources" in s && n(0, r = s.Resources);
  }, [r];
}
class Rt extends te {
  constructor(e) {
    super(), ee(this, e, qt, kt, Y, { Resources: 0 });
  }
}
const $t = async function(t, e, n) {
  var r = {};
  return r.ResourceName = t, r.RequirementName = e, r.RequirementCapacity = n, console.log(r), await (await fetch("http://localhost:5266/api/Resource", {
    headers: { accept: "*/*", "content-type": "application/json; charset=utf-8" },
    method: "POST",
    body: JSON.stringify(r)
  })).json();
}, Et = async function() {
  return await (await fetch("http://localhost:5266/api/Resource", { method: "GET" })).json();
};
function Je(t, e, n) {
  const r = t.slice();
  return r[11] = e[n], r;
}
function Pe(t) {
  let e, n = (
    /*item*/
    t[11].name + ""
  ), r, s;
  return {
    c() {
      e = m("option"), r = F(n), e.__value = s = /*item*/
      t[11].name, e.value = e.__value;
    },
    m(l, i) {
      R(l, e, i), a(e, r);
    },
    p(l, i) {
      i & /*Requirements*/
      2 && n !== (n = /*item*/
      l[11].name + "") && le(r, n), i & /*Requirements*/
      2 && s !== (s = /*item*/
      l[11].name) && (e.__value = s, e.value = e.__value);
    },
    d(l) {
      l && k(e);
    }
  };
}
function Nt(t) {
  let e, n, r, s, l, i, c, o, u, f, g, y, v, w, N, A, T, P, x, h, _, p, $, E, q, W, J, I, K, L, z;
  f = new Rt({
    props: { Resources: (
      /*Resources*/
      t[0]
    ) }
  });
  let H = (
    /*Requirements*/
    t[1]
  ), C = [];
  for (let d = 0; d < H.length; d += 1)
    C[d] = Pe(Je(t, H, d));
  return {
    c() {
      e = m("h1"), e.textContent = "Resources", n = S(), r = m("table"), s = m("tr"), s.innerHTML = "<th>Resource Name</th>", l = S(), i = m("tr"), i.innerHTML = "<th>Requirement Name</th>", c = S(), o = m("tr"), o.innerHTML = "<th>Requirement Capacity</th>", u = S(), Z(f.$$.fragment), g = S(), y = m("div"), v = m("div"), v.textContent = "Name:", w = S(), N = m("input"), A = S(), T = m("div"), P = m("div"), P.textContent = "Requirement Name:", x = S(), h = m("select");
      for (let d = 0; d < C.length; d += 1)
        C[d].c();
      _ = S(), p = m("div"), $ = m("div"), $.textContent = "Requirement Amount:", E = S(), q = m("input"), W = S(), J = m("div"), I = m("button"), I.textContent = "Submit", b(v, "class", "form-label"), b(N, "type", "text"), b(N, "class", "form-control"), b(N, "id", "resourceName"), b(y, "class", "mb-3"), b(P, "class", "form-label"), /*selected*/
      t[2] === void 0 && pe(() => (
        /*select_change_handler*/
        t[7].call(h)
      )), b(T, "class", "mb-3"), b($, "class", "form-label"), b(q, "type", "number"), b(q, "class", "form-control"), b(q, "id", "requirementCapacity"), b(p, "class", "mb-3"), b(I, "class", "btn btn-primary"), b(J, "class", "mb-3");
    },
    m(d, j) {
      R(d, e, j), R(d, n, j), R(d, r, j), a(r, s), a(r, l), a(r, i), a(r, c), a(r, o), a(r, u), G(f, r, null), R(d, g, j), R(d, y, j), a(y, v), a(y, w), a(y, N), oe(
        N,
        /*resourceNameInput*/
        t[3]
      ), R(d, A, j), R(d, T, j), a(T, P), a(T, x), a(T, h);
      for (let ne = 0; ne < C.length; ne += 1)
        C[ne].m(h, null);
      Ee(
        h,
        /*selected*/
        t[2]
      ), R(d, _, j), R(d, p, j), a(p, $), a(p, E), a(p, q), oe(
        q,
        /*resourceCapacityInput*/
        t[4]
      ), R(d, W, j), R(d, J, j), a(J, I), K = !0, L || (z = [
        Q(
          N,
          "input",
          /*input0_input_handler*/
          t[6]
        ),
        Q(
          h,
          "change",
          /*select_change_handler*/
          t[7]
        ),
        Q(
          q,
          "input",
          /*input1_input_handler*/
          t[8]
        ),
        Q(
          I,
          "click",
          /*addResource*/
          t[5]
        )
      ], L = !0);
    },
    p(d, [j]) {
      const ne = {};
      if (j & /*Resources*/
      1 && (ne.Resources = /*Resources*/
      d[0]), f.$set(ne), j & /*resourceNameInput*/
      8 && N.value !== /*resourceNameInput*/
      d[3] && oe(
        N,
        /*resourceNameInput*/
        d[3]
      ), j & /*Requirements*/
      2) {
        H = /*Requirements*/
        d[1];
        let D;
        for (D = 0; D < H.length; D += 1) {
          const Re = Je(d, H, D);
          C[D] ? C[D].p(Re, j) : (C[D] = Pe(Re), C[D].c(), C[D].m(h, null));
        }
        for (; D < C.length; D += 1)
          C[D].d(1);
        C.length = H.length;
      }
      j & /*selected, Requirements*/
      6 && Ee(
        h,
        /*selected*/
        d[2]
      ), j & /*resourceCapacityInput*/
      16 && He(q.value) !== /*resourceCapacityInput*/
      d[4] && oe(
        q,
        /*resourceCapacityInput*/
        d[4]
      );
    },
    i(d) {
      K || (M(f.$$.fragment, d), K = !0);
    },
    o(d) {
      U(f.$$.fragment, d), K = !1;
    },
    d(d) {
      d && k(e), d && k(n), d && k(r), B(f), d && k(g), d && k(y), d && k(A), d && k(T), ge(C, d), d && k(_), d && k(p), d && k(W), d && k(J), L = !1, X(z);
    }
  };
}
function St(t, e, n) {
  const r = async function() {
    n(0, s = await Et());
  };
  let s = [];
  r();
  const l = async function() {
    n(1, i = await qe());
  };
  let i = [];
  l();
  let c, o = "", u = 0;
  const f = async function() {
    await $t(o, c, u), n(3, o = ""), n(4, u = 0), r();
  };
  function g() {
    o = this.value, n(3, o);
  }
  function y() {
    c = Ze(this), n(2, c), n(1, i);
  }
  function v() {
    u = He(this.value), n(4, u);
  }
  return [
    s,
    i,
    c,
    o,
    u,
    f,
    g,
    y,
    v
  ];
}
class jt extends te {
  constructor(e) {
    super(), ee(this, e, St, Nt, Y, {});
  }
}
function Ot(t) {
  let e;
  return {
    c() {
      e = m("h1"), e.textContent = "Page not found";
    },
    m(n, r) {
      R(n, e, r);
    },
    p: O,
    i: O,
    o: O,
    d(n) {
      n && k(e);
    }
  };
}
class Lt extends te {
  constructor(e) {
    super(), ee(this, e, null, Ot, Y, {});
  }
}
const At = {
  "/requirements": Te,
  "/requirements/:reqName": Te,
  "/resources": jt,
  "*": Lt
};
function Ct(t) {
  let e, n, r, s;
  return r = new mt({ props: { routes: At } }), {
    c() {
      e = m("nav"), e.innerHTML = `<a class="navbar-brand col-sm-3 col-md-2 mr-0" href="#">Company name</a> 
    <input class="form-control form-control-dark w-100" type="text" placeholder="Search" aria-label="Search"/> 
    <ul class="navbar-nav px-3"><li class="nav-item text-nowrap"><a class="nav-link" href="#">Sign out</a></li></ul>`, n = S(), Z(r.$$.fragment), b(e, "class", "navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0");
    },
    m(l, i) {
      R(l, e, i), R(l, n, i), G(r, l, i), s = !0;
    },
    p: O,
    i(l) {
      s || (M(r.$$.fragment, l), s = !0);
    },
    o(l) {
      U(r.$$.fragment, l), s = !1;
    },
    d(l) {
      l && k(e), l && k(n), B(r, l);
    }
  };
}
class Tt extends te {
  constructor(e) {
    super(), ee(this, e, null, Ct, Y, {});
  }
}
const xt = new Tt({
  target: document.getElementById("app")
});
export {
  xt as default
};
//# sourceMappingURL=main.js.map
